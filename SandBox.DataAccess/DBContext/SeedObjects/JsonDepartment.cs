namespace SandBox.DataAccess.DBContext.SeedObjects
{
    class JsonDepartment
    {
        #region Properties

        public string Code { get; set; }
        public string Title { get; set; }

        public JsonRegion Region { get; set; }

        #endregion
    }
}
