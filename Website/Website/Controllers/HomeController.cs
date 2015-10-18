// This file is part of AlarmWorkflow.
// 
// AlarmWorkflow is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// AlarmWorkflow is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with AlarmWorkflow.  If not, see <http://www.gnu.org/licenses/>.

using AlarmWorkflow.Website.Reports.Models;
using System.Web.Mvc;

namespace AlarmWorkflow.Website.Reports.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// GET: /
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// GET: /About
        /// </summary>
        /// <returns></returns>
        public ActionResult About()
        {
            return View();
        }

        /// <summary>
        /// GET: /Settings
        /// </summary>
        /// <returns></returns>
        public ActionResult Settings()
        {
            SettingsData data = new SettingsData();
            ViewBag.Save = false;
            return View(data);
        }

        /// <summary>
        /// Post: /Settings
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Settings(SettingsData data)
        {
            if (ModelState.IsValid)
            {
                data.Save();
            }

            ViewBag.Save = true;

            return View(data);
        }
    }
}
