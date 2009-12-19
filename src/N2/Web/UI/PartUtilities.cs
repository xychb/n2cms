﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using N2.Definitions;
using System.IO;
using N2.Edit;

namespace N2.Web.UI
{
    /// <summary>
    /// Helper methods for drag'n'drop support.
    /// </summary>
    public static class PartUtilities
    {
        public const string TypeAttribute = "data-type";
        public const string PathAttribute = "data-item";
        public const string ZoneAttribute = "data-zone";
        public const string AllowedAttribute = "data-allowed";

        public static string GetAllowedNames(string zoneName, IEnumerable<ItemDefinition> definitions)
        {
            List<string> allowedDefinitions = new List<string>();
            foreach (ItemDefinition potentialChild in definitions)
            {
                allowedDefinitions.Add(potentialChild.Discriminator);
            }
            return string.Join(",", allowedDefinitions.ToArray());
        }

        public static void WriteTitleBar(TextWriter writer, IEditManager editManager, ItemDefinition definition, ContentItem item)
        {
            writer.Write("<div class='titleBar ");
            writer.Write(definition.Discriminator);
            writer.Write("'>");

            var returnUrl = Url.Parse(editManager.GetPreviewUrl(item)).AppendQuery("edit", "drag");
            WriteCommand(writer, "Edit part", "command edit", Url.Parse(editManager.GetEditExistingItemUrl(item)).AppendQuery("returnUrl", returnUrl).Encode());
            WriteCommand(writer, "Delete part", "command delete", Url.Parse(editManager.GetDeleteUrl(item)).AppendQuery("returnUrl", returnUrl).Encode());
            WriteTitle(writer, definition);

            writer.Write("</div>");
        }

        private static void WriteCommand(TextWriter writer, string title, string @class, string url)
        {
            writer.Write("<a title='" + title + "' class='" + @class + "' href='");
            writer.Write(url);
            writer.Write("'></a>");
        }

        private static void WriteTitle(TextWriter writer, ItemDefinition definition)
        {
            writer.Write("<span class='title' style='background-image:url(");
            writer.Write(Url.ToAbsolute(definition.IconUrl));
            writer.Write(");'>");
            writer.Write(definition.Title);
            writer.Write("</span>");
        }
    }
}
