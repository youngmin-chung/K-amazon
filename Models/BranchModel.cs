using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


namespace K_amazon.Models
{
    // define a model as an object that we use to send information to the database,
    // to perform business calculations and to render a view.
    public class BranchModel
    {
        private AppDbContext _db;
        public BranchModel(AppDbContext context)
        {
            _db = context;
        }

        public List<Branch> GetThreeClosestBranches(float? lat, float? lng)
        {
            List<Branch> branchDetails = null;
            try
            {
                //var latParam = new SqlParameter("@lat", lat);
                //var lngParam = new SqlParameter("@lng", lng);
                var query = _db.Branches.FromSqlRaw("EXECUTE dbo.pGetThreeClosestBranches {0}, {1}", lat, lng);
                branchDetails = query.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return branchDetails;
        }
    }
}
