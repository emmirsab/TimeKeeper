﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Models;
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
        public IHttpActionResult Get([FromUri] Header h)
        {
            var list = TimeKeeperUnit.Calendar.Get()
                .Header(h)
                .Select(x => TimeKeeperFactory.Create(x))
                .ToList();
            Logger.Log("Returned all days", "INFO");
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
                Logger.Log($"No such day with id {id}");
                return NotFound();
            }
            else
            {
                Logger.Log($"Returned day with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(day));
            }
        }

        /// <summary>
        /// Insert new Day
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] CalendarModel day)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var message = "Failed inserting new day, ";

                }
                TimeKeeperUnit.Calendar.Insert(TimeKeeperFactory.Create(day));
                TimeKeeperUnit.Save();
                Logger.Log("Inserted new day", "INFO");
                return Ok(day);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update chosen Day
        /// </summary>
        /// <param name="day"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] CalendarModel day, int id)
        {
            try
            {
                if (TimeKeeperUnit.Calendar.Get(id) == null) return NotFound();
                TimeKeeperUnit.Calendar.Update(day, id);
                if (TimeKeeperUnit.Save())
                {
                    Logger.Log($"Updated day with id {id}", "INFO");
                    return Ok(day);
                }
                else
                {
                    throw new Exception($"Failed updating day with id {id}, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
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
                    Logger.Log($"No day found with id {id}");
                    return NotFound();
                }

                /* Tried to delete all of the foreign key contraint items
                 * within the delete function, however it requires more
                 * attetion, and debugging, for now left alone until
                 * more consultation needed
                DetailsController dc = new DetailsController();
                foreach(var item in TimeKeeperUnit.Details.Get().Where(x => x.Day.Id == day.Id)){
                    dc.Delete(item.Id);
                }
                */

                TimeKeeperUnit.Calendar.Delete(day);
                TimeKeeperUnit.Save();
                Logger.Log($"Deleted day with id {id}", "INFO");
                return Ok();
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
