using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mensajeria_Linux.EntityFramework.Data;
using Mensajeria_Linux.EntityFramework.Entities;
using Mensajeria_Linux.EntityFramework.Helpers;
using Mensajeria_Linux.EntityFramework.Models.InfoEmail;
using Mensajeria_Linux.EntityFramework.Models.InfoTeam;
using Mensajeria_Linux.Services.Interfaces;
using System.Net.Mail;
using System.Net;
using Mensajeria_Linux.Controllers.Email.Models;

namespace Mensajeria_Linux.Services
{
    /// <summary>
    /// Capa servicio de Email
    /// </summary>
    public class InfoEmailService : IInfoEmailService
    {
        private NotificationContext _dbCntext;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor de la capa servicio de Email
        /// </summary>
        /// <param name="dbCntext"></param>
        /// <param name="mapper"></param>
        public InfoEmailService (NotificationContext dbCntext, IMapper mapper)
        {
            _dbCntext = dbCntext;
            _mapper = mapper;
        }
        /// <summary>
        /// Creación de una Email, coprueba que no exista uno con el mimso email de origen
        /// </summary>
        /// <param name="model"></param>
        /// <param name="id"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> CreateInfoEmail (CreateInfoEmailRequest model, int id)
        {
            if (await _dbCntext.infoEmail.AnyAsync(x =>  x.emailOrigen == model.emailOrigen))
            {
                return 0;
            }
            InfoEmail infoEmail = _mapper.Map<InfoEmail>(model);
            infoEmail.agenciaId = id;
            infoEmail.created  = DateTime.Now;
            
            _dbCntext.infoEmail.Add(infoEmail);
            try
            {
                await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
                return infoEmail.id;
            }catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// Eliminación de una Email
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> DeleteInfoEmail (int id, int agenciaId)
        {
            InfoEmail? infoEmail = await _getInfoEmailByIdAndAgenciaId(id, agenciaId);
            if (infoEmail != null)
            {
                _dbCntext?.infoEmail.Remove(infoEmail);
                return  await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
                
            }
            else
            {
                throw new RepositoryExceptions($"No existe InfoEmail de usuario con id {id}");
            }
        }
        /// <summary>
        /// Actualización de un email de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <param name="agenciaId"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public async Task<int> UpdateEmailTeams (int id, UpdateInfoEmailRequest model,int agenciaId)
        {
            InfoEmail? infoEmail = await _getInfoEmailByIdAndAgenciaId(id, agenciaId);
            // Validation
            if (model.emailOrigen != infoEmail.emailOrigen && await _dbCntext.infoEmail.AnyAsync(x => x.emailOrigen == model.emailOrigen))
                throw new RepositoryExceptions($"{model.emailOrigen} ya existe.");

            _mapper.Map(model, infoEmail);
            _dbCntext.infoEmail.Update(infoEmail);
            return await _dbCntext.SaveChangesAsync().ConfigureAwait(true);
        }
        /// <summary>
        /// Obtiene el registro de un email por su id y el id de la agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>InfoEmail</returns>
        /// <exception cref="KeyNotFoundException">InfoEmail no se ha encontrado en la base de datos</exception>
        private async Task<InfoEmail> _getInfoEmailByIdAndAgenciaId (int id, int agenciaId)
        {
            InfoEmail? infoEmail = await _dbCntext.infoEmail
                .AsNoTracking()
                .Where(x => x.id == id && x.agenciaId == agenciaId)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            if (infoEmail == null)
            {
                throw new KeyNotFoundException("InfoEmail no se ha encontrado en la base de datos");
            }
            return infoEmail;
        }
        /// <summary>
        /// Obtener un email por id y agencia
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenciaId"></param>
        /// <returns>Un registro Email</returns>
        public async Task<InfoEmail> GetInfoEmailById (int id, int agenciaId)
        {
            InfoEmail? infoEmail = await _dbCntext.infoEmail
                     .AsNoTracking()
                     .Where(x => x.id == id && x.agenciaId == agenciaId)
                     .FirstOrDefaultAsync().ConfigureAwait(true);

            if (infoEmail == null)
            {
                throw new KeyNotFoundException("infoEmail not found");
            }
            else
            {
                return infoEmail;
            }
        }
        /// <summary>
        /// Obtención de todos los email de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de Emails de una agencia</returns>
        public Task<IEnumerable<InfoEmail>> GetAllInfoEmailByAgencia (int id)
        {
            return _getAllInfoEmailByAgencia(id);
        }
        /// <summary>
        /// Obtención de todos los email de una agencia
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Lista de Emails de una agencia</returns>
        /// <exception cref="KeyNotFoundException">No existen los datos de ese email</exception>
        public async Task<IEnumerable<InfoEmail>> _getAllInfoEmailByAgencia (int id)
        {
            IEnumerable<InfoEmail>? infoEmail = await _dbCntext.infoEmail
                     .AsNoTracking()
                     .Where(x => x.agenciaId == id)
                     .ToArrayAsync().ConfigureAwait(true);

            if (infoEmail == null)
            {
                throw new KeyNotFoundException("No existen los datos de ese email");
            }
            else
            {
                return infoEmail;
            }


        }
        /// <summary>
        /// Obtener información de un email por nombre y agencia
        /// </summary>
        /// <param name="agenciaId"></param>
        /// <param name="emailOrigen"></param>
        /// <returns>Un registro de Email</returns>
        public async Task<InfoEmail> GetInfoEmailByAgenciaIdAndTipo (int agenciaId, string emailOrigen)
        {
            InfoEmail? infoEmail = await _dbCntext.infoEmail
                .AsNoTracking()
                .Where(x => x.agenciaId == agenciaId && x.emailOrigen == emailOrigen)
                .FirstOrDefaultAsync().ConfigureAwait(true);
            return infoEmail;
            
        }
        /// <summary>
        /// Enviar un email con los datos ya rellenos, se envía un Email a cada uno de los destinos. cada email enviado contiene la plantilla y si existen los ficheros
        /// </summary>
        /// <param name="plantilla"></param>
        /// <param name="infoEmail"></param>
        /// <param name="emailsDestino"></param>
        /// <param name="titulo"></param>
        /// <param name="ficheros"></param>
        /// <returns>
        ///     Exito: int > 0
        ///     Fracaso: int = 0
        /// </returns>
        public  int EnviarEmailConPlantillaADestino (string plantilla, InfoEmail infoEmail, List<DatosEmail> emailsDestino, string titulo, List<DatosFichero>? ficheros)
        {
            var client = new SmtpClient();
            client.Host = infoEmail.host;
            client.Port = infoEmail.port;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(infoEmail.emailOrigen, infoEmail.emailTokenPassword);
            foreach (DatosEmail emailDestino in emailsDestino)
            {
                using (var message = new MailMessage(
                   from: new MailAddress(infoEmail.emailOrigen, infoEmail.emailNombre),
                   to: new MailAddress(emailDestino.email, emailDestino.name)
                   ))
                {
                    message.Subject = titulo;
                    message.Body = plantilla;
                    message.IsBodyHtml = true;
                    if (ficheros != null)
                    {
                        foreach (DatosFichero fichero in ficheros)
                        {
                            byte[] Bytes = Convert.FromBase64String(fichero.contenidoFichero);

                            message.Attachments.Add(CrearFicheroPorExtension(Bytes, fichero.nombreFichero, fichero.extension));
                        }
                    }                    
                    try
                    {
                        client.Send(message);
                    }catch (Exception ex)
                    {
                        return 0;
                    }
                    
                }
            }

            return 1;
        }
        /// <summary>
        /// Crea los ficheros por extensión en una porción de memoria
        /// </summary>
        /// <param name="Bytes"></param>
        /// <param name="nombre"></param>
        /// <param name="extension"></param>
        /// <returns>Attachment</returns>
        private Attachment CrearFicheroPorExtension (byte[] Bytes, string nombre, string extension)
        {
            switch (extension)
            {
                case "pdf":
                    return new Attachment(new MemoryStream(Bytes), nombre, "application/pdf");
                case "xlsx":
                    return new Attachment(new MemoryStream(Bytes), nombre, "application/xlsx");
                case "xls":
                    return new Attachment(new MemoryStream(Bytes), nombre, "application/xls");
                case "zip":
                    return new Attachment(new MemoryStream(Bytes), nombre, "application/zip");
                default:
                    return null;
            }
        }
    }
}
