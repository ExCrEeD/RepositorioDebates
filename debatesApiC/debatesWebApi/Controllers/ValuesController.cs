using debatesWebApi.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace debatesWebApi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public void Get()
        {

        }

        // GET api/values/5
        public byte[] Get(int id)
        {
            Image imageIn = Image.FromFile("C:\\Users\\ExCrEeD\\Documents\\RepositorioDebates\\debatesApiC\\debatesWebApi\\images\\accent.png");
            return Utilidades.Utilidades.ImageToByteArray(imageIn);

        }

        // POST api/values
        public void Post([FromBody]DTODebate newDebate)
        {
            Image imagendebate = Utilidades.Utilidades.byteArrayToImage(newDebate.ImageByteArray);
            string imagePath = @AppDomain.CurrentDomain.BaseDirectory + "images\\" + Utilidades.Utilidades.GetRandomString() + "." + newDebate.extensionImage;
            imagendebate.Save(imagePath, newDebate.extensionImage.ToUpper() == "PNG" ? ImageFormat.Png : ImageFormat.Png);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}