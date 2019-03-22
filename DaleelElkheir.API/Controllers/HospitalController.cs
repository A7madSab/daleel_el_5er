using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.Hospitals;
using DaleelElkheir.BLL.Services.Hospitals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace DaleelElkheir.API.Controllers
{
    
    public class HospitalController : ApiController
    {
        private readonly IHospitalService HospitalService;

        public HospitalController(IHospitalService _HospitalService)
        {
            this.HospitalService = _HospitalService;
        }

        [HttpPost]
        public IHttpActionResult GetHospitals(BaseRequest model)
        {
            if(ModelState.IsValid)
            {
                var hospitals = HospitalService.GetHospitals();
                List<HospitalModel> hospitalList = new List<HospitalModel>();

                foreach (var item in hospitals)
                {
                    var hospitalModel = new HospitalModel
                    {
                        ID = item.ID,
                        Name=model.Lang=="ar"?item.NameAr:item.NameEn,
                        Title=model.Lang=="ar"?item.TitleAr:item.TitleEn,
                        Governorate=model.Lang=="ar"?item.City.Governorate.NameAr:item.City.Governorate.NameEn,
                        City =model.Lang=="ar"?item.City.NameAr:item.City.NameEn
                    };
                    hospitalList.Add(hospitalModel);
                }

                return Ok(new BaseResponse (hospitalList));
            }
            return BadRequest(ModelState);

        }

        [HttpPost]
        public IHttpActionResult GetHospitalDetails(BaseRequestID request)
        {
            if (ModelState.IsValid)
            {

                var bloodBankContact = HospitalService.GetHospitalContacts(x => x.HospitalID == request.ID).Select(m => new HospitalContactModel { ContactName = m.ContactName, ContactNumber = m.ContactNumber}).ToList();
                var bloodBank = HospitalService.GetHospital(request.ID);
                var BankModel = new HospitalDetailModel
                {
                    Name = request.Lang == "ar" ? bloodBank.NameAr : bloodBank.NameEn,
                    Governorate = request.Lang == "ar" ? bloodBank.City.Governorate.NameAr : bloodBank.City.Governorate.NameEn,
                    City = request.Lang == "ar" ? bloodBank.City.NameAr : bloodBank.City.NameEn,
                    Title = request.Lang == "ar" ? bloodBank.TitleAr : bloodBank.TitleEn,
                    Description = request.Lang == "ar" ? bloodBank.DescriptionAr : bloodBank.DescriptionEn,
                    HospitalContacts = bloodBankContact
                };

                return Ok(new BaseResponse(BankModel));
            }
            return BadRequest(ModelState);

        }


        [HttpPost]
        public IHttpActionResult GetFilterHospitals(FilterHospitalRequest model)
        {
            if (ModelState.IsValid)
            {
                var bloodBanks = HospitalService.GetHospitals();

                if (model.RegionID != null)
                {
                    bloodBanks = bloodBanks.Where(x => x.CityID == model.RegionID).ToList();
                }
                if (model.GovernorateID != null)
                {
                    bloodBanks = bloodBanks.Where(x => x.City.GovernorateID == model.GovernorateID).ToList();
                }

                List<HospitalModel> bloodBankList = new List<HospitalModel>();

                foreach (var item in bloodBanks)
                {
                    var BankModel = new HospitalModel
                    {
                        ID = item.ID,
                        Name = model.Lang == "ar" ? item.NameAr : item.NameEn,
                        Title = model.Lang == "ar" ? item.TitleAr : item.TitleEn,
                        Governorate = model.Lang == "ar" ? item.City.Governorate.NameAr : item.City.Governorate.NameEn,
                        City = model.Lang == "ar" ? item.City.NameAr : item.City.NameEn
                    };
                    bloodBankList.Add(BankModel);
                }

                return Ok(new BaseResponse(bloodBankList));
            }
            return BadRequest(ModelState);

        }

    }
}
