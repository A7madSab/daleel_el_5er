using DaleelElkheir.API.Models;
using DaleelElkheir.API.Models.BloodBanks;
using DaleelElkheir.BLL.Services.BloodBanks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace DaleelElkheir.API.Controllers
{
    
    public class BloodBankController : ApiController
    {
        private readonly IBloodBankService BloodBankService;

        public BloodBankController(IBloodBankService _BloodBankService)
        {
            this.BloodBankService = _BloodBankService;
        }

        [HttpPost]
        public IHttpActionResult GetBloodBanks(BaseRequest model)
        {
            if(ModelState.IsValid)
            {
                var bloodBanks=BloodBankService.GetBloodBanks();
                List<BloodBankModel> bloodBankList = new List<BloodBankModel>();

                foreach (var item in bloodBanks)
                {
                    var BankModel = new BloodBankModel
                    {
                        ID = item.ID,
                        Name=model.Lang=="ar"?item.NameAr:item.NameEn,
                        Title=model.Lang=="ar"?item.TitleAr:item.TitleEn,
                        Governorate=model.Lang=="ar"?item.City.Governorate.NameAr:item.City.Governorate.NameEn,
                        City =model.Lang=="ar"?item.City.NameAr:item.City.NameEn
                    };
                    bloodBankList.Add(BankModel);
                }

                return Ok(new BaseResponse (bloodBankList));
            }
            return BadRequest(ModelState);

        }

        [HttpPost]
        public IHttpActionResult GetBloodBankDetails(BaseRequestID request)
        {
            if (ModelState.IsValid)
            {

                var bloodBankContact = BloodBankService.GetBloodBankContacts(x => x.BloodBankID == request.ID).Select(m => new BloodBankContactModel { ContactName = m.ContactName, ContactNumber = m.ContactNumber}).ToList();
                var bloodBank = BloodBankService.GetBloodBank(request.ID);
                var BankModel = new BloodBankDetailModel
                {
                    Name = request.Lang == "ar" ? bloodBank.NameAr : bloodBank.NameEn,
                    Governorate = request.Lang == "ar" ? bloodBank.City.Governorate.NameAr : bloodBank.City.Governorate.NameEn,
                    City = request.Lang == "ar" ? bloodBank.City.NameAr : bloodBank.City.NameEn,
                    Title = request.Lang == "ar" ? bloodBank.TitleAr : bloodBank.TitleEn,
                    Description = request.Lang == "ar" ? bloodBank.DescriptionAr : bloodBank.DescriptionEn,
                    BloodBankContacts = bloodBankContact
                };

                return Ok(new BaseResponse(BankModel));
            }
            return BadRequest(ModelState);

        }


        [HttpPost]
        public IHttpActionResult GetFilterBloodBanks(FilterBloodBankRequest model)
        {
            if (ModelState.IsValid)
            {
                var bloodBanks = BloodBankService.GetBloodBanks();

                if (model.RegionID != null)
                {
                    bloodBanks = bloodBanks.Where(x => x.CityID == model.RegionID).ToList();
                }
                if (model.GovernorateID != null)
                {
                    bloodBanks = bloodBanks.Where(x => x.City.GovernorateID == model.GovernorateID).ToList();
                }

                List<BloodBankModel> bloodBankList = new List<BloodBankModel>();

                foreach (var item in bloodBanks)
                {
                    var BankModel = new BloodBankModel
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
