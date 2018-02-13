﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TimeKeeper.API.Models;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.API.Controllers
{
    public class TeamsController : BaseController
    {
        public IHttpActionResult Get()
        {
            var list = TimeUnit.Teams.Get().ToList()
                           .Select(t => TimeFactory.Create(t))
                           .ToList();
            return Ok(list); //Ok - status 200
        }

        public IHttpActionResult Get(string id)
        {
            Team team = TimeUnit.Teams.Get(id);
            if (team == null)
                return NotFound();
            else
                return Ok(TimeFactory.Create(team));
        }

        public IHttpActionResult Post([FromBody] Team team)
        {
            try
            {
                TimeUnit.Teams.Insert(team);
                TimeUnit.Save();
                return Ok(team);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Put([FromBody] Team team, string id)
        {
            try
            {
                if (TimeUnit.Teams.Get(id) == null) return NotFound();
                TimeUnit.Teams.Update(team, id);
                TimeUnit.Save();
                return Ok(team);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public IHttpActionResult Delete(string id)
        {
            try
            {
                Team team = TimeUnit.Teams.Get(id);
                if (team == null) return NotFound();
                TimeUnit.Teams.Delete(team);
                TimeUnit.Save();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
