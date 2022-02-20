using Assets.Lineker.Assets.REST_Client.Enums;
using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class IsClothesAvailable
    {
        public static bool IsClothingAvailable(Mapa map)
        {
            if (map == Mapa.FOREST)
            {
                var forestTime = PlayerPrefs.GetInt("ForestProgressTime");
                return forestTime != 0;
            }

            if (map == Mapa.CITY)
            {
                var cityTime = PlayerPrefs.GetInt("CityProgressTime");
                return cityTime != 0;
            }

            if (map == Mapa.RIVER)
            {
                var riverTime = PlayerPrefs.GetInt("RiverProgressTime");
                return riverTime != 0;
            }

            return false;
        }
    }
}