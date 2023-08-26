using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


public class Well : Entity, IWaterSource
{
    public void Fill()
    {
        if(InteractionManager.Instance.SelectedItem is WateringCan)
        {
            (InteractionManager.Instance.SelectedItem as WateringCan).Fill();
        }
    }    
}
