﻿using System;
using N2.Edit.Web;
using N2.Collections;
using N2.Edit.FileSystem.Items;
using N2.Resources;

namespace N2.Edit.FileSystem
{
    public partial class Directory1 : EditPage
    {
		protected override void RegisterToolbarSelection()
		{
			string script = GetToolbarSelectScript("preview");
			Register.JavaScript(this, script, ScriptPosition.Bottom, ScriptOptions.ScriptTags);
		}

        protected void Page_Load(object sender, EventArgs e)
        {
            Title = Selection.SelectedItem.Title;
            hlNewFile.NavigateUrl = Engine.EditManager.GetEditNewPageUrl(Selection.SelectedItem, Engine.Definitions.GetDefinition(typeof(File)), null, CreationPosition.Below);

            lblDirectories.Text = Selection.SelectedItem.GetChildren(new TypeFilter(typeof(Directory))).Count.ToString();
            lblFiles.Text = Selection.SelectedItem.GetChildren(new TypeFilter(typeof(File))).Count.ToString();
        }
    }
}
