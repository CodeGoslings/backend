using MRCModel.Data;
using MRCModel.Models;


namespace MRCModel.Repositories
{
    public class MRC_Repository
    {
        private readonly HACS_Context _context;

        // Constructor that accepts the DbContext
        public MRC_Repository(HACS_Context context)
        {
            _context = context;
        }

         // Fetch all Requests
        public List<Request> FetchAllRequests()
        {
            try
            {
                List<Request> requestsList = _context.Requests.ToList();
                return requestsList;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error fetching all requests: {ex.Message}");
                return new List<Request>(); // Return an empty list if there is an error
            }
        }

        // Add new Request
        public bool AddNewRequest(Request request)
        {
            try
            {
                // Simulating database insert operation
                _context.Requests.Add(request);
                _context.SaveChanges();
                return true; // If no exceptions, operation was successful
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error adding new request: {ex.Message}");
                return false; // Return false to indicate failure
            }
        }

        // Find Request by id
        public Request FindRequestById(int requestId)
        {
            try
            {
                // Search the simulated database for the request with the given id
                var request = _context.Requests.FirstOrDefault(r => r.requestId == requestId);
                if (request == null)
                {
                    throw new Exception("Request with such ID not found.");
                }
                return request;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error finding request by ID {requestId}: {ex.Message}");
                return null; // Return null in case of an error
            }
        }

        // Edit Request by id
        public bool EditRequestById(Request request)
        {
            try
            {
                FindRequestById(request.requestId);
                if (request == null)
                {
                    throw new Exception("Request with such ID not found.");
                }
                DeleteRequestById(request.requestId);
                AddNewRequest(request);
                return true; // Return true to indicate successful update
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error editing request by ID {request.requestId}: {ex.Message}");
                return false; // Return false to indicate failure
            }
        }

        // Delete Request by id
        public bool DeleteRequestById(int requestId)
        {
            try
            {
                var request = FindRequestById(requestId);
                if (request == null)
                {
                    return false; // Return false if request is not found
                }

                // Remove the request from the simulated database
                _context.Requests.Remove(request);
                return true; // Return true to indicate successful deletion
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error deleting request by ID {requestId}: {ex.Message}");
                return false; // Return false to indicate failure
            }
        }

        // Edit RequestStatus (use EditRequestById)
        public bool EditRequestStatus(int requestId, RequestStatus status)
        {
            try
            {
                var request = FindRequestById(requestId);
                if (request == null)
                {
                    throw new Exception("Request with such ID not found.");
                }

                // Change the request status
                request.status = status;
                return true; // Return true to indicate successful status update
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error editing status for request ID {requestId}: {ex.Message}");
                return false; // Return false to indicate failure
            }
        }

        // Get Request History by submittedBy (userId)
        public List<Request> GetRequestHistory(string userId)
        {
            try
            {
                // Filter requests by the submittedBy field (in this case, userId)
                var history = _context.Requests.Where(r => r.submittedBy == userId).ToList();
                return history; // Return the filtered list
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error getting request history for user {userId}: {ex.Message}");
                return new List<Request>(); // Return an empty list if there's an error
            }
        }

        public List<User> FetchAllUsers()
        {
            return null;
        }
    }
}
