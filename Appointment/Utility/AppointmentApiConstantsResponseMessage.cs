using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Utility
{
    public static class AppointmentApiConstantsResponseMessage
    {
        public const string appointment_null_of_get_list = "ERROR: There is no data available.";
        public const string appointment_get_all_success = "SUCCESS: gets successfully.";
        public const string appointment_see_try_catch = "ERROR: Something went wrong. Please check the try-catch block.";
        public const string appointment_insert_success_message = "Data inserted successfully !";
        public const string appointment_inserted_failed_message = "Data is not inserted successfully !";
        public const string appointment_deleted_failed_message = "Data is not deleted !";
        public const string appointment_delete_success_message = "Data is deleted successfully !";
        public const string appointment_update_success_message = "Data is updated successfully !";
        public const string appointment_update_failed_message = "Data is not updated !";

    }
}
