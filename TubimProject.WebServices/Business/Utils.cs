namespace TubimProject.WebServices.Business
{
    public static class Utils
    {

        public static void MaddeAdiCevirici(string orjinal, ref string modifiye)
        {
            switch (orjinal)
            {
                case "AFYON":
                    modifiye="AFYON SAKIZI";
                    break;
                case "AKİNETON":
                case "LYRICA":
                case "METHADON":
                case "NARKOTİK/PSİKOTROP TABLETLER":
                case "REGAPEN":
                case "RİVOTRİL":
                case "TRAMADOL":
                case "XANAX":
                case "SUBOXONE":
                    modifiye="SENTETİK ECZA";
                    break;
                case "ASİT ANHİDRİT":
                    modifiye="ASETİK ANHİDRİT";
                    break;
                case "EXTACY":
                    modifiye="ECSTASY";
                    break;
                case "KUBAR ESRAR":
                case "REÇİNE ESRAR":
                case "TOZ ESRAR":
                    modifiye="ESRAR";
                    break;
                case "MORFİN":
                    modifiye="BAZMORFİN";
                    break;
                case "SALVİA DİVİNORİUM":
                    modifiye="SALVİA DİVİNOROUM";
                    break;
                case "SENTETİK KANNABİNOİD/BONZAİ":
                    modifiye="SENTETİK KANNABİNOİD";
                    break;
                case "SKUNK":
                    modifiye="SKANK";
                    break;
                default:
                    modifiye=orjinal;
                    break;
            }
        }
    }
}
