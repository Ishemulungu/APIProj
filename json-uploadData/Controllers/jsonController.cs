using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;

namespace json_uploadData.Controllers
{
    public class jsonController : Controller
    {
        // GET: json
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uploadjson(HttpPostedFileBase filejson)
        {
            jsonDeviceDataEntities sd = new jsonDeviceDataEntities();
            {
                if (!filejson.FileName.EndsWith(".json"))
                {
                    ViewBag.errorMsg = "Not a JSON file!";
                }

                else
                {
                    filejson.SaveAs(Server.MapPath("~/empfolder" + Path.GetFileName(filejson.FileName)));
                    StreamReader reader = new StreamReader(Server.MapPath("~/empfolder" + Path.GetFileName(filejson.FileName)));
                    string jsondata = reader.ReadToEnd();

                    List<deviceData> deviceList = JsonConvert.DeserializeObject<List<deviceData>>(jsondata);
                    foreach (var item in deviceList)
                    {
                        item.index.ToString();
                        item.dmac.ToString();
                        item.refpower.ToString();
                        item.uuid.ToString();
                        item.majorID.ToString();
                        item.rssi.ToString();
                        item.minorID.ToString();
                        item.type.ToString();
                        item.time.ToString();

                        sd.deviceDatas.Add(item);
                        sd.SaveChanges();
                    }

                    ViewBag.successMsg = "Data added to the database.";
                }
            }

            return View("Index");
        }
    }
}