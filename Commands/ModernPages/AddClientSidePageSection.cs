﻿using Microsoft.SharePoint.Client;
using OfficeDevPnP.Core.Pages;
using SharePointPnP.PowerShell.CmdletHelpAttributes;
using SharePointPnP.PowerShell.Commands.Base.PipeBinds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace SharePointPnP.PowerShell.Commands.ModernPages
{
    [Cmdlet(VerbsCommon.Add, "PnPClientSidePageSection")]
    [CmdletHelp("Adds a new section to a Client-Side page",
      Category = CmdletHelpCategory.ModernPages)]
    [CmdletExample(
        Code = @"PS:> Add-PnPClientSidePageSection -Page 'MyPage' -SectionTemplate OneColumn",
        Remarks = "Adds a new one-column section to the Client-Side page 'MyPage'",
        SortOrder = 1)]
    [CmdletExample(
        Code = @"PS:> Add-PnPClientSidePageSection  -Page 'MyPage' -SectionTemplate ThreeColumn -Order 10",
        Remarks = "Adds a new Three columns section to the page 'MyPage' with an order index of 10",
        SortOrder = 2)]
    public class AddClientSidePageSection : PnPWebCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0, HelpMessage = "The name of the page or the page in-memory instance.")]
        public ClientSidePagePipeBind Page;

        [Parameter(Mandatory = true, HelpMessage = "Specifies the columns template to use for the section.")]
        public CanvasSectionTemplate SectionTemplate;

        [Parameter(Mandatory = false, HelpMessage = "Sets the order of the section. (Default = 1)")]
        public int Order = 1;


        protected override void ExecuteCmdlet()
        {
            var clientSidePage = Page?.GetPage(ClientContext);

            if (clientSidePage != null)
            {
                clientSidePage.AddSection(SectionTemplate, Order);
                clientSidePage.Save();
            }
            else
            {
                // If the client side page object cannot be found
                throw new Exception($"Page {Page} cannot be found.");
            }

        }
    }
}
