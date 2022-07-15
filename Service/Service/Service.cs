using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;
using t = System.Timers;

namespace Service
{

    public partial class Service : ServiceBase
    {
        // evitar concorrencia de trads
        protected static readonly object padlock = new object();

        public Service()
        {
            InitializeComponent();
        }

        private static string EventViewerName
        {
            get
            {
                return "Boletos de Aluguel";
            }
        }
        //Quando o usuário interage com o serviço no servidor

        protected override void OnStart(string[] args)
        {
            Iniciar();
        }

        internal static void Iniciar()
        {
            try
            {
                var t = new Task(() =>
                {
                    _envioNotificacaoTimer_Elapsed(null, null);
                });
                t.Start();
                // Caso o timer esteja desligado ele inicia
                if (!EnvioNotificacaoTimer.Enabled)
                {
                    EnvioNotificacaoTimer.Enabled = true;
                }

                RegistrarEvento($"Apuração: Serviço iniciado", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException != null ? $"{ex.Message} {ex.InnerException.Message}" : ex.Message;
                RegistrarEvento($"Apuração: {msg}", EventLogEntryType.Error);
            }
        }

        private static t.Timer _envioNotificacaoTimer;
        protected static t.Timer EnvioNotificacaoTimer
        {
            get
            {
                //somente uma pessoa consegue acessar esse pedaço de código
                lock (padlock)
                {
                    if (_envioNotificacaoTimer == null)
                    {
                        _envioNotificacaoTimer = new t.Timer(3600000);
                        _envioNotificacaoTimer.Elapsed += _envioNotificacaoTimer_Elapsed;
                        _envioNotificacaoTimer.AutoReset = true;
                        _envioNotificacaoTimer.Start();
                    }
                }
                return _envioNotificacaoTimer;
            }
        }

        private static void _envioNotificacaoTimer_Elapsed(object sender, t.ElapsedEventArgs e)
        {
            //Se a hora for igual a 8 horas da manhã inicia o projeto
            if (DateTime.Now.Hour == 8)
            {
                Iniciar();
            }
        }

        protected override void OnStop()
        {
            try
            {
                EnvioNotificacaoTimer.Stop();
                EnvioNotificacaoTimer.Dispose();
                _envioNotificacaoTimer = null;
                EnvioNotificacaoTimer.Stop();
                EnvioNotificacaoTimer.Dispose();
                _envioNotificacaoTimer = null;
                RegistrarEvento($"Apuração: Serviço finalizado", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException != null ? $"{ex.Message} {ex.InnerException.Message}" : ex.Message;
                RegistrarEvento($"Apuração: {msg}", EventLogEntryType.Error);
            }
        }

        protected override void OnPause()
        {
            try
            {
                EnvioNotificacaoTimer.Stop();
                _envioNotificacaoTimer.Stop();
                RegistrarEvento($"Apuração: Serviço parado", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException != null ? $"{ex.Message} {ex.InnerException.Message}" : ex.Message;
                RegistrarEvento($"Apuração: {msg}", EventLogEntryType.Error);
            }
        }

        protected override void OnShutdown()
        {
            try
            {
                EnvioNotificacaoTimer.Stop();
                EnvioNotificacaoTimer.Dispose();
                _envioNotificacaoTimer = null;
                RegistrarEvento($"Apuração: Serviço desligado", EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                string msg = ex.InnerException != null ? $"{ex.Message} {ex.InnerException.Message}" : ex.Message;
                RegistrarEvento($"Apuração: {msg}", EventLogEntryType.Error);
            }
        }

        public static void RegistrarEvento(string mensagem, EventLogEntryType tipo)
        {
            string sourceEvent = EventViewerName;

            if (!EventLog.SourceExists(sourceEvent))
            {
                EventLog.CreateEventSource(sourceEvent, EventViewerName);
            }

            EventLog.WriteEntry(sourceEvent, mensagem, tipo);
        }
    }
}
