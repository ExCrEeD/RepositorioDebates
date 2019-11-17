using debatesWebApi.Context;
using debatesWebApi.DTO;
using debatesWebApi.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace debatesWebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DebatesController : ApiController
    {
        // GET api/debates
       
        DataStore db = new DataStore();
        public IEnumerable<object> Get(int idUsuario) // cambiar metodo get para obtener rate por usuario activo en el sistema
        {

            //actualizar estado de debates ya vencidos
            foreach (var item in db.Debates)
            {
                if (item.FechaVencimiento <= DateTime.Now)
                {
                    item.Estado =true;
                }
            }
            db.SaveChanges();
            var query = from a in db.Debates
                        join b in db.Users on a.Autor equals b.Id
                        select new
                        {
                            Id = a.Id,
                            Titulo = a.Titulo,
                            Tema = a.Tema,
                            Autor = a.Autor,
                            AutorName = b.Name + " " + b.SecondName,
                            FechaPublicacion = a.FechaPublicacion,
                            FechaVencimiento = a.FechaVencimiento,
                            Estado = a.Estado,
                            Image = a.Image,
                            Rate = (from c in db.Ratings where c.DebateId == a.Id && c.CommentId == 0 && c.AutorId == idUsuario select c.Rate).FirstOrDefault(),
                            RatingCount = (from d in db.Ratings where d.DebateId == a.Id && d.CommentId == 0 select d).Count(),
                            //Average = ((from d in db.Ratings where d.DebateId == a.Id && d.CommentId == 0 select d).Count() ==0)?0:(from d in db.Ratings where d.DebateId == a.Id && d.CommentId == 0 select d.Rate).Sum() /
                            //(from d in db.Ratings where d.DebateId == a.Id && d.CommentId == 0 select d).Count() 
                        };
            return query.ToList();
        }

        // POST api/debates
        public Response Post([FromBody]DTODebate newDebate)
        {
            Response answer = new Response();
            try
            {
                var query = from a in db.Users
                            where a.Id == newDebate.Autor
                            select a;
                if (query.Count()==0)
                {
                    answer.State = 1;
                    answer.Message = "El usuario no se encuentra registrado";
                }
                else
                {
                    Debates debate = new Debates(){ Autor= newDebate.Autor, Titulo = newDebate.Titulo,
                        Tema = newDebate.Tema,FechaVencimiento = newDebate.FechaVencimiento
                      };
                    if (newDebate.ImageByteArray.Length > 0)
                    {
                        Image imagendebate = Utilidades.Utilidades.byteArrayToImage(newDebate.ImageByteArray);
                        string imageName = "images\\" + Utilidades.Utilidades.GetRandomString() + "." + newDebate.extensionImage;
                        string imagePath = @AppDomain.CurrentDomain.BaseDirectory + imageName;
                        imagendebate.Save(imagePath, newDebate.extensionImage.ToUpper() == "PNG" ? ImageFormat.Png : ImageFormat.Png);
                        debate.Image = imageName;
                        debate.FechaPublicacion = DateTime.Now;
                    }
                    db.Debates.Add(debate);
                    db.SaveChanges();
                    answer.State = 0;
                    answer.Message = "Debate subido correctamente";
                }
            }
            catch (Exception ex)
            {
                answer.State = 1;
                answer.Message = ex.Message;
                return answer;
            }
            return answer;
        }

        // PUT api/debates/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/debates/5
        public void Delete(int id)
        {
        }
    }
}
