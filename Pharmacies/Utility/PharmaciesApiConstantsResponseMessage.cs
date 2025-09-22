using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacies.Utility
{
    public static class PharmaciesApiConstantsResponseMessage
    {
        public const string pharmacies_null_of_get_list = "ERROR: There is no data available.";
        public const string pharmacies_get_all_success = "SUCCESS: gets successfully.";
        public const string pharmacies_see_try_catch = "ERROR: Something went wrong. Please check the try-catch block.";
        public const string pharmacies_insert_success_message = "Data inserted successfully !";
        public const string pharmacies_inserted_failed_message = "Data is not inserted successfully !";
        public const string pharmacies_deleted_failed_message = "Data is not deleted !";
        public const string pharmacies_delete_success_message = "Data is deleted successfully !";
        public const string pharmacies_update_success_message = "Data is updated successfully !";
        public const string pharmacies_update_failed_message = "Data is not updated !";

    }
}
