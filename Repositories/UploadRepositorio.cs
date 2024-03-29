using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using PROJETO.Models;

namespace tst.Repositorio {
    public class UploadRepositorio {

        EventShareContext context = new EventShareContext ();
        public string Upload (IFormFile arquivo, string savingFolder) {

            var pathToSave = Path.Combine (Directory.GetCurrentDirectory (), savingFolder);

            if (arquivo.Length > 0) {
                var fileName = ContentDispositionHeaderValue.Parse (arquivo.ContentDisposition).FileName.Trim ('"');
                var fullPath = Path.Combine (pathToSave, fileName);

                using (var stream = new FileStream (fullPath, FileMode.Create)) {
                    arquivo.CopyTo (stream);
                }                    

                return savingFolder + "/" +fileName;
            } else {
                return null;    
            }  
        }

    }
}