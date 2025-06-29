using Microsoft.AspNetCore.Mvc;
using Section7.Models.IValidationObjectModel;
using Section7.Models.Validation;
using static System.Net.Mime.MediaTypeNames;

namespace Section7.Controllers
{
    [Route("model")]
    public class ModelValidationController : Controller
    {
        /*
         
         * Using : Form Data
         
                    Data
          ==============================
            Id          : 47 
            Name        : Test User
            Address     : Test Location
            City        : CT
            Region      : RTS
            PostalCode  : 500155 
            Country     : CDS
            Phone       : 8562457854
            Email       : sam@sample.com

            // Additionally we have password and confirm password fields as well

         */

        [Route("register")]
        public IActionResult Index(Register register)
        {
            return Content(register.ToString());
        }

        [Route("validate")]
        public IActionResult Validate(Register register)
        {
            /*
             * One simple example of the input and output
             * Its time consuming to record all possible flows in validation.
             * This gives idea on ModelState -> isValid and Error Collection.
             * For all other combinations - I leave it for your valid imagination
             
                                            Data
                            =======================================

                                Id              :   47
                                Address         :   Test Location
                                City            :   CT
                                Region          :   RTS
                                PostalCode      :   500155
                                Phone           :   8562457854
                                Country         :   CDS
                                Email           :   sam@sample.com
                                Password        :   Sample123
                                ConfirmPassword :   Sample123

                                Result :
                                    [
                                        "The Name field is required."
                                    ]

             */

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return BadRequest(errors);
            }
            return Content(register.ToString());
        }

        /// <summary>
        /// This method is used to run all other scenarios for practise
        /// And this uses a copy of Registry class so feel free to edit and play around
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [Route("scenarios")]
        public IActionResult PlayGround(RegisterPlayGround register)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return BadRequest(errors);
            }
            return Content(register.ToString());
        }

        /// <summary>
        /// This method is used to run With in Class Validation using IValidatableObject
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        [Route("validatableObject")]
        public IActionResult ValidatableObject(RegisterModelInlineValidate register)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                return BadRequest(errors);
            }
            return Content(register.ToString());
        }

    }
}
