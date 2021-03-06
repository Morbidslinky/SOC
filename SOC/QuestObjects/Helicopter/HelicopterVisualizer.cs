﻿using SOC.QuestObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SOC.Core.Classes.InfiniteHeaven;
using SOC.UI;
using SOC.Forms.Pages;
using System.Windows.Forms;
using SOC.Classes.Common;
using SOC.Core.Classes.Route;
using SOC.QuestObjects.Enemy;

namespace SOC.QuestObjects.Helicopter
{
    class HelicopterVisualizer : NonLocationalVisualizer
    {
        public HelicopterVisualizer(HelicopterControl control) : base(control, control.panelQuestBoxes) { }

        private List<string> routes = new List<string>();

        public override void DrawMetadata(Metadata meta)
        {
            HelicopterControl heliControl = (HelicopterControl)detailControl;
            heliControl.SetMetadata((HelicopterMetadata)meta);
        }

        public override Metadata GetMetadataFromControl()
        {
            return new HelicopterMetadata((HelicopterControl)detailControl);
        }

        public override QuestBox NewBox(QuestObject qObject)
        {
            return new HelicopterBox((Helicopter)qObject, routes);
        }

        public override Detail NewDetail(Metadata meta, IEnumerable<QuestObject> qObjects)
        {
            return new HelicopterDetail(qObjects.Cast<Helicopter>().ToList(), (HelicopterMetadata)meta);
        }

        public override void SetDetailsFromSetup(Detail detail, CoreDetails core)
        {
            // Routes
            List<string> heliRoutes = new List<string>();
            if (core.routeName != "NONE")
                heliRoutes = new RouteManager().GetRouteNames(core.routeName);
            heliRoutes.AddRange(EnemyInfo.GetCP(core.CPName).CPheliRoutes);

            routes = heliRoutes;

            List<Helicopter> qObjects = detail.GetQuestObjects().Cast<Helicopter>().ToList();
            int heliCount = (routes.Count > 0 ? 1 : 0);
            int objectCount = qObjects.Count;

            for (int i = 0; i < heliCount; i++)
            {
                if (i >= objectCount) // add
                {
                    qObjects.Add(new Helicopter(i));
                }
            }

            for (int i = objectCount - 1; i >= heliCount; i--) //remove
            {
                qObjects.RemoveAt(i);
            }

            detail.SetQuestObjects(qObjects.Cast<QuestObject>().ToList());
        }
    }
}
