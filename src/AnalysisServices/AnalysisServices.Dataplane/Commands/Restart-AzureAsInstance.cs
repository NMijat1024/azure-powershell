﻿// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using System.Management.Automation;
using Microsoft.Azure.Commands.AnalysisServices.Dataplane.Models;
using Microsoft.Azure.Commands.AnalysisServices.Dataplane.Properties;
using Microsoft.Azure.Commands.Common.Authentication.Abstractions;

namespace Microsoft.Azure.Commands.AnalysisServices.Dataplane
{
    /// <summary>
    /// Cmdlet to log into an Analysis Services environment
    /// </summary>
    [Cmdlet("Restart", ResourceManager.Common.AzureRMConstants.AzurePrefix + "AnalysisServicesInstance", SupportsShouldProcess = true)]
    [Alias("Restart-AzureAsInstance", "Restart-AzAsInstance")]
    [OutputType(typeof(bool))]
    public class RestartAzureAnalysisServer : AsAzureDataplaneCmdletBase
    {
        [Parameter(Mandatory = false)]
        public SwitchParameter PassThru { get; set; }

        public RestartAzureAnalysisServer()
        {
        }

        protected override IAzureContext DefaultContext
        {
            get
            {
                // Nothing to do with Azure Resource Management context
                return null;
            }
        }

        protected override string DataCollectionWarning
        {
            get
            {
                return Resources.ARMDataCollectionMessage;
            }
        }

        protected override void InitializeQosEvent()
        {
            // nothing to do here.
        }

        public override void ExecuteCmdlet()
        {
            if (!ShouldProcess(Instance, Resources.RestartingAnalysisServicesServer))
            {
                return;
            }

            var restartEndpoint = string.Format(AsAzureEndpoints.RestartEndpointPathFormat, ServerName);

            using (var message = AsAzureDataplaneClient.CallPostAsync(restartEndpoint).Result)
            {
                message.EnsureSuccessStatusCode();
                if (PassThru.IsPresent)
                {
                    WriteObject(true);
                }
            }
        }
    }
}
