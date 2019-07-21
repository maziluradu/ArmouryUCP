using ArmouryUCP.WebAPI.Services;
using ArmouryUCP.WebAPI.Services.Interfaces;
using DSharpPlus;
using DSharpPlus.EventArgs;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Http;
using Unity;

namespace ArmouryUCP.WebAPI
{
    public static class WebApiConfig
    {
        private static string BOT_VERSION = "0.1.1";

        private static int messagesCount = 0;
        private static long messagesCountTimestamp = 0;

        public static async void RegisterAsync(HttpConfiguration config)
        {
            config.EnableCors();

            // Web API configuration and services  
            // Configure Web API to use only bearer token authentication.  
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API configuration and services
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Models.Player, Models.Dtos.PlayerDto>().ForMember(
                    dest => dest.Faction,
                    opt => opt.MapFrom(src => src.Leader > 0 ? SharedResources.Factions[src.Leader] : SharedResources.Factions[src.Member])
                );
            });

            var container = new UnityContainer();
            container.RegisterType<IPlayerService, PlayerService>();
            container.RegisterType<IHouseService, HouseService>();
            container.RegisterType<IVehicleService, VehicleService>();
            container.RegisterType<IBusinessService, BusinessService>();
            container.RegisterType<IServerService, ServerService>();
            container.RegisterType<IComplaintService, ComplaintService>();
            config.DependencyResolver = new UnityResolver(container);
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "ArmouryApi",
                routeTemplate: "api/{controller}/{id}/{action}",
                defaults: new { id = RouteParameter.Optional }
            );

            var discordClient = new DiscordClient(new DiscordConfiguration
            {
                Token = "NTYyNjAyOTM3NjE2OTU3NDQ5.XPVWeQ.-pl0ERlWIRgLQPSEBKtyTFiKCFA",
                TokenType = TokenType.Bot
            });

            discordClient.MessageCreated += OnMessageCreated;

            await discordClient.ConnectAsync();
            await Task.Delay(-1);

        }

        private static string GenerateRandomResponseForMessage(string message, string authorName)
        {
            var blankResponses = new string[] {
                "?",
                "Da, {0}?",
                "M-ai strigat?",
                "M-ai strigat, {0}?",
                "Da, {0}, cu ce te ajut?"
            };

            var angryResponses = new string[] {
                "Hai ca ma enervati.",
                "N-ai treaba, {0}?",
                "{0}, nu te dor degetele?",
                "Eu imi dau demisia..",
                "{0}, iei ban.",
                "Ti-o iei, {0}!"
            };

            var howYouDoingResponses = new string[]
            {
                "Sunt ok, {0}, tu?",
                "Calculez niste statistici. Tu?",
                "Calculez niste statistici, {0}. Tu?",
                "Tin serverele in picioare. Tu?",
                "Tin serverele in picioare, {0}. Tu?",
                "Bine, tu?",
                "Ma gandesc sa intru pe server. Tu?"
            };

            var thankYouResponses = new string[]
            {
                "Cu placere, {0}.",
                "Sa traiesti, {0}.",
                "Ma bucur ca te-am putut ajuta, {0}."
            };

            var slangs = new string[]
            {
                "prietene",
                "amice",
                "bro",
                "capitane"
            };

            if (messagesCount >= 5)
            {
                messagesCountTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + 10;
                return string.Format(angryResponses[new Random().Next(angryResponses.Length)], authorName);
            }
            else
            {
                if ((Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds < messagesCountTimestamp)
                    messagesCount++;
                else
                    messagesCount = 0;

                messagesCountTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds + 2;
            }

            if (message == " versiune")
                return string.Format("Sunt versiunea " + BOT_VERSION + ", " + slangs[new Random().Next(slangs.Length)] + ".", authorName);

            if (message == " Jay e prost?")
                return "Da";

            if (message.Contains("ce faci?") || message.Contains("ce mai faci?") || message.Contains("cf?") || message.Contains("cf") || message.Contains("cmz?") || message.Contains("cmz") || message.Contains("cmf?") || message.Contains("cmf"))
                return string.Format(howYouDoingResponses[new Random().Next(howYouDoingResponses.Length)], authorName);

            if (message.Contains("mersi") || message.Contains("multumesc") || message.Contains("multam"))
                return string.Format(thankYouResponses[new Random().Next(thankYouResponses.Length)], authorName);

            if (message.Length == 0)
                return string.Format(blankResponses[new Random().Next(blankResponses.Length)], authorName);

            return "?";
        }

        private static async Task OnMessageCreated(MessageCreateEventArgs e)
        {
            var content = Regex.Replace(e.Message.Content, "<.*?>", string.Empty);

            var slurs = new string[]
            {
                "mue",
                "muie",
                "pula",
                "pizda",
                "fmm",
                "fgm",
                "mortii ma-tii",
                "mortii matii",
                "ma-tii",
                "matii",
                "handicapat",
                "bou"
            };

            var slursResponses = new string[]
            {
                "{0}, limbajul.",
                "{0}, ai grija la limbaj.",
                "{0}, ce-i la gura ta?",
                "Vorbeste frumos, {0}.",
                "Ai grija la limbaj, {0}.",
                "{0}, putin respect, ok?"
            };

            if (slurs.Any(slur => content.Contains(" " + slur) || content.Contains(slur + " ") || content == slur))
                await e.Message.RespondAsync(string.Format(slursResponses[new Random().Next(slursResponses.Length)], e.Message.Author.Username));

            if (e.MentionedUsers.Any(mu => mu.IsCurrent))
            {
                await e.Message.RespondAsync(GenerateRandomResponseForMessage(content, e.Message.Author.Username));
            }
        }
    }
}
