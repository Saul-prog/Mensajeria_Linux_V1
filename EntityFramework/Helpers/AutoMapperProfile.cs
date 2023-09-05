using AutoMapper;
using Mensajeria_Linux.Services;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Models.InfoTeam;
using Mensajeria_Linux.EntityFramework.Models.InfoWhatsApp;
using Mensajeria_Linux.EntityFramework.Models.Agencias;
using Mensajeria_Linux.EntityFramework.Models.InfoSMS;
using Mensajeria_Linux.EntityFramework.Models.InfoEmail;
using Mensajeria_Linux.EntityFramework.Models.Plantillas;

namespace Mensajeria_Linux.EntityFramework.Helpers
{
    /// <summary>
    /// Clase AutoMapperProfile
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Construye las relaciones entre las diferentes clases, al hacerlo desde una que actualiza se omiten los nulos, vacíos y espacios en blanco para no elimianr datos anteriores
        /// </summary>
        public AutoMapperProfile()
        {
            CreateMap< Agencia, CreateAgenciaRequest>();
            CreateMap< Agencia, UpdateAgenciaRequest>()
               .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap< CreateAgenciaRequest, Agencia>();
            CreateMap<UpdateAgenciaRequest, Agencia>()
               .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap<InfoTeams, CreateInfoTeamsRequest>();
            CreateMap<InfoTeams, UpdateInfoTeamsRequest>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap< CreateInfoTeamsRequest, InfoTeams>();
            CreateMap< UpdateInfoTeamsRequest, InfoTeams>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap<InfoWhatsApp, CreateInfoWhatsAppRequest>();
            CreateMap<InfoWhatsApp, UpdateInfoWhatsAppRequest>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap<CreateInfoWhatsAppRequest, InfoWhatsApp>();
            CreateMap<UpdateInfoWhatsAppRequest, InfoWhatsApp>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap<InfoSMS, CreateInfoSMSRequest>();
            CreateMap<InfoSMS, UpdateInfoSMSRequest>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap<CreateInfoSMSRequest, InfoSMS>();
            CreateMap<UpdateInfoSMSRequest, InfoSMS>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap<InfoEmail, CreateInfoEmailRequest>();
            CreateMap<InfoEmail, UpdateInfoEmailRequest>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap<CreateInfoEmailRequest, InfoEmail>();
            CreateMap<UpdateInfoEmailRequest, InfoEmail>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap<Plantillas, CreatePlantillaRequest>();
            CreateMap<Plantillas, UpdatePlantillaRequest>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));
            CreateMap< CreatePlantillaRequest, Plantillas>();
            CreateMap<UpdatePlantillaRequest, Plantillas>()
                .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       //ignore null and empty string properties
                       if (prop == null) return false;
                       if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;


                       return true;
                   }
                 ));

        }
    }
}
