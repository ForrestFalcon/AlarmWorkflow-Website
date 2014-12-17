﻿// This file is part of AlarmWorkflow.
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

using AlarmWorkflow.Shared.Core;

namespace AlarmWorkflow.Website.Reports.Areas.Display.Models
{
    /// <summary>
    /// Represents the result from the "GetLastOperation" service call.
    /// </summary>
    public class GetLastOperationData
    {
        /// <summary>
        /// Gets/sets whether or not the call was successful.
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// Gets/sets the <see cref="Operation"/>-object that was returned, if any.
        /// </summary>
        public Operation op { get; set; }
    }
}