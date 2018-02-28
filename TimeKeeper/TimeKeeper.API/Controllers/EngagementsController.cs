﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.API.Helper;
using TimeKeeper.DAL;
using TimeKeeper.DAL.Entities;

namespace TimeKeeper.API.Controllers
{
    public class EngagementsController : BaseController
    {
        /// <summary>
        /// Get all Engagements
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get([FromUri] Header h)
        {
            var list = TimeKeeperUnit.Engagements.Get()
                .Where(x => x.Employee.FullName.Contains(h.filter))
                .Header(h)
                .ToList()
                .Select(x => TimeKeeperFactory.Create(x))
                .ToList();
            Logger.Log("Returned all members", "INFO");
            return Ok(list);
        }

        /// <summary>
        /// Get specific Engagement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Get(int id)
        {
            Engagement member = TimeKeeperUnit.Engagements.Get(id);
            if (member == null)
            {
                Logger.Log($"No such engagement with id {id}");
                return NotFound();
            }
            else
            {
                Logger.Log($"Returned engagement with id {id}", "INFO");
                return Ok(TimeKeeperFactory.Create(member));
            }
        }

        /// <summary>
        /// Insert new Engagement
        /// </summary>
        /// <param name="engagement"></param>
        /// <returns></returns>
        public IHttpActionResult Post([FromBody] Engagement engagement)
        {
            try
            {
                TimeKeeperUnit.Engagements.Insert(engagement);
                if (TimeKeeperUnit.Save())
                {
                    Logger.Log("Inserted new engagement", "INFO");
                    return Ok(engagement);
                }
                else
                {
                    throw new Exception("Failed inserting engagement, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update chosen Engagement
        /// </summary>
        /// <param name="engagement"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Put([FromBody] Engagement engagement, int id)
        {
            try
            {
                if (TimeKeeperUnit.Engagements.Get(id) == null)
                {
                    Logger.Log($"No such engagement with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Engagements.Update(engagement, id);
                if (TimeKeeperUnit.Save())
                {
                    Logger.Log($"Updated engagement with id {id}", "INFO");
                    return Ok(engagement);
                }
                else
                {
                    throw new Exception($"Failed updating engagement with id {id}, wrong data sent");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "ERROR", ex);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete chosen Engagement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                Engagement member = TimeKeeperUnit.Engagements.Get(id);
                if (member == null)
                {
                    Logger.Log($"No such engagement with id {id}");
                    return NotFound();
                }
                TimeKeeperUnit.Engagements.Delete(member);
                TimeKeeperUnit.Save();
                Logger.Log($"Deleted engagement with id {id}", "INFO");
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
