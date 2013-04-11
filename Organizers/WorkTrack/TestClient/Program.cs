using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Knightrunner.Library.Core;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkTrackServiceReference.WorkTrackServiceClient workTrack = new WorkTrackServiceReference.WorkTrackServiceClient("BasicHttpBinding_IWorkTrackService");
            //var project = workTrack.CreateProject();

            //project.Id = Guid.NewGuid();
            //project.Name = "Test project";
            //project.Description = "A test project created in a client";
            //workTrack.InsertProject(project);

            var originalProject = workTrack.GetProject(new Guid("539F3772-9525-4B30-8AAD-BB70DB9508FB"));
            var updatedProjcet = ObjectCopier.Clone(originalProject);
            updatedProjcet.Description = "Modified description2";
            workTrack.UpdateProject(updatedProjcet, originalProject);
        }
    }
}
