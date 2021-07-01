namespace ScoreSheetScanner.App.Helper
{
    class MeetingItem
    {
        public string HomeTeam { get; set; }
        public string GuestTeam { get; set; }
        public string Date { get; set; }
        public string MeetingID { get; set; }
        public string MeetingUri { get; set; }

        public override string ToString()
        {
            return $"{HomeTeam} - {GuestTeam} - {Date}";
        }
    }
}