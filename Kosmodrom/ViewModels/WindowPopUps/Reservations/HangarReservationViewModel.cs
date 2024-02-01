using CommunityToolkit.Mvvm.Messaging;
using Kosmodrom.Helpers;
using Kosmodrom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kosmodrom.ViewModels.WindowPopUps.Reservations
{
    public class HangarReservationViewModel : SingleReservationViewModel<HangarReservation>
    {
        public int numberId;

        public HangarReservationViewModel(int numberId) : base()
        {
            this.numberId = numberId;
        }

        public override void SaveAndSend()
        {
            if (Buttons[0] != 1 && Buttons[1] != 1)
            {
                WeakReferenceMessenger.Default.Send(new ReservationMessenger<HangarReservation>(numberId, new HangarReservation()
                {
                    StartTime = Date,
                    StartHour = Buttons[0],
                    ReservationTime = Buttons[1] - Buttons[0],
                    HangarId = SelectedThing,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    LastEditedAt = DateTime.Now
                }));
            }
            Clear();
        }
        public override void InitReservations()
        {
            ClearButtons();

            List<HangarReservation> AllList = Database.HangarReservations.Where(item => item.IsActive == true && item.StartTime.Day == Date.Day && item.StartTime.Month == Date.Month && item.StartTime.Year == Date.Year).ToList();
            if (AllList.Count > 0)
            {
                for (int i = 0; i < ThingsAmount; i++)
                {
                    List<HangarReservation> resevationsList = AllList.Where(item => item.HangarId == i + 1).ToList();
                    List<int> list = new();
                    foreach (HangarReservation item in resevationsList)
                    {
                        for (int? q = item.StartHour; q < item.ReservationTime + item.StartHour; q++)
                        {
                            if (q != null)
                            {
                                list.Add((int)q);
                            }
                        }
                    }
                    Reservations.Add(list);
                }
                for (int i = 0; i < Reservations.Count; i++)
                {
                    List<int> ints = Reservations[i];
                    for (int j = 0; j < ints.Count; j++)
                    {
                        ReservationMesseges[ints[j]] = ReservationMesseges[ints[j]] + " " + (i + 1).ToString();
                    }
                }
            }
            Reservations = new();
            RefreshButtons();
        }

        public override int ThingAmount()
        {
            return Database.SpaceportSupports.FirstOrDefault(item => item.IsActive == true && item.Thing.Equals("Hangars")).Amount;
        }
    }
}

