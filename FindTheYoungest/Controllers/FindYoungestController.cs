using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FindYoungest.Models;

namespace FindYoungest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FindYoungestController : ControllerBase
    {
        private int FindTheYoungestAge(Person[] age)
        {
            //find the youngest age using array.Min()
            int[] youngestAge = new int[age.Length];
            int a = 0;
            foreach (var personAge in age)
            {
                youngestAge[a] = personAge.Age;
                a++;
            }
            return youngestAge.Min();

        }


        private String[] FindYoungestNames(Person[] persons, int youngestAge)
        {

            var found = persons.Where(x => x.Age == youngestAge);
            // get the length of found if the return value is 1 or more
            int l = 0;
            foreach (var value in found)
            {
                l = l + 1;//result of the length of array
            }

            var NamesOfYoungest = new String[l];//declare the string array
            int i = 0;
            foreach (var value in found)
            {
                NamesOfYoungest[i] = value.Name;//result of searching the youngest in array
                i++;


            }

            return NamesOfYoungest;


        }

        [HttpPost("[action]")]
        // [ActionName("PostAndFindYoungest")]
        public ActionResult<Person> PostandFindYoungest([FromBody] Person[] persons)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (persons.Length != 0)
                    {

                        int youngestAge = FindTheYoungestAge(persons);
                        return Ok(FindYoungestNames(persons, youngestAge));
                    }
                    else
                    {
                        throw new ArgumentException("Data is null");
                    }
                }
                else
                {
                    throw new ArgumentException("invalid ModelState");
                }
            }
            catch (ArgumentException e)
            {
                return BadRequest("Bad Request " + e.Message);
            }
        }

    }


}
