﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class DaysController : BaseController
    {
        /// <summary>
        /// Get all Days
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var list = TimeKeeperUnit.Calendar.Get().ToList().Select(x => TimeKeeperFactory.Create(x)).ToList();
            Utility.Log("Returned all days");
            return Ok(list);
        }

        /// <summary>
        /// Get specific Day
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            Day day = TimeKeeperUnit.Calendar.Get(id);
            if (day == null)
            {
                Utility.Log($"No such day with id {id}");
                return NotFound();
            }
            else
            {
                Utility.Log($"Returned day with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(day));
            }
        }

        /// <summary>
        /// Insert new Day
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] Day day)
        {
            try
            {
                TimeKeeperUnit.Calendar.Insert(day);
                if (TimeKeeperUnit.Save())
                {
                    Utility.Log("Inserted new day", "INFO");
                    return Ok(day);
                }
                else
                {
                    throw new Exception("Failed inserting new day, wrong data sent");
                }
                
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update chosen Day
        /// </summary>
        /// <param name="day"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] Day day, int id)
        {
            try
            {
                if (TimeKeeperUnit.Calendar.Get(id) == null) return NotFound();
                TimeKeeperUnit.Calendar.Update(day, id);
                if (TimeKeeperUnit.Save())
                {
                    Utility.Log($"Updated day with id {id}", "INFO");
                    return Ok(day);
                }
                else
                {
                    throw new Exception($"Failed updating day with id {id}, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete chosen Day
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Day day = TimeKeeperUnit.Calendar.Get(id);
                if (day == null)
                {
                    Utility.Log($"No day found with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Calendar.Delete(day);
                TimeKeeperUnit.Save();
                Utility.Log($"Deleted day with id {id}", "INFO");
                return Ok();
            }
            catch (Exception ex)
            {
                Utility.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}