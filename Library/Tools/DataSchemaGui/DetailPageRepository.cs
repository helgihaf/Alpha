using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.Controls.PageSelection;
using System.Windows.Forms;

namespace DataSchemaGui
{
    public class DetailPageRepository : IDetailPageRepository
    {
        Dictionary<Type, IDetailPage> dictionary = new Dictionary<Type, IDetailPage>();

        public IDetailPage GetPageOf(IDirector director, object dataObject)
        {
            if (director == null)
            {
                throw new ArgumentNullException("director");
            }

            if (dataObject == null)
            {
                return null;
            }

            IDetailPage detailPage;
            if (!dictionary.TryGetValue(dataObject.GetType(), out detailPage))
            {
                detailPage = CreateDetailPage(dataObject.GetType(), director);
                dictionary.Add(dataObject.GetType(), detailPage);
            }

            return detailPage;
        }

        private IDetailPage CreateDetailPage(Type dataObjectType, IDirector director)
        {
            IDetailPage result = null;
            Control parentControl = director.PageParentControl;
            Control control = null;

            if (dataObjectType == typeof(Knightrunner.Library.Database.Schema.Project.DataSchemaProject))
            {
                control = new DetailPages.ProjectPage();
            }
            //if (dataObjectType == typeof(Knightrunner.Library.Database.Schema.DataSchema))
            //{
            //    control = new DetailPages.DataSchemaPage();
            //}
            //else if (dataObjectType == typeof(Knightrunner.Library.Database.Schema.TargetSystem))
            //{
            //    control = new DetailPages.TargetSystemPage();
            //}
            //else if (dataObjectType == typeof(Knightrunner.Library.Database.Schema.ColumnType))
            //{
            //    control = new DetailPages.ColumnTypePage();
            //}

            if (control != null)
            {
                if (parentControl != null)
                {
                    parentControl.Controls.Add(control);
                    control.Dock = DockStyle.Fill;
                    control.BringToFront();
                }

                result = (IDetailPage)control;
            }

            return result;
        }
    }
}
