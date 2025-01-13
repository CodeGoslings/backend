using System;
using System.Collections.Generic;
using System.Linq;

namespace MRCModel
{
    public class DatabaseManager
    {
        // Simulated in-memory database for requests
        private List<Request> requestsDatabase = new List<Request>();

        // Fetch all Requests
        public List<Request> FetchAllRequests()
        {
            try
            {
                return requestsDatabase;
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
                requestsDatabase.Add(request);
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
                var request = requestsDatabase.FirstOrDefault(r => r.requestId == requestId);
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
                requestsDatabase.Remove(request);
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
                var history = requestsDatabase.Where(r => r.submittedBy == userId).ToList();
                return history; // Return the filtered list
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error getting request history for user {userId}: {ex.Message}");
                return new List<Request>(); // Return an empty list if there's an error
            }
        }
    }
}
