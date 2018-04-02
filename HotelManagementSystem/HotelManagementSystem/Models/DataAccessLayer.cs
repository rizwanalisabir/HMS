using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagementSystem.Models
{
    public class DataAccessLayer
    {
        string connectionString = @"data source=DESKTOP-7JC4UNT\SQLEXPRESS;initial catalog=HMSDB;persist security info=True;integrated security=True;";
        
        //To View all employees details  
        public IEnumerable<tblRoomInfo> GetAllRooms()
        {
            try
            {
                List<tblRoomInfo> roomslist = new List<tblRoomInfo>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPRoomManager", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("OpCode", SqlDbType.NVarChar, 10).Value = "selectall";
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        tblRoomInfo room = new tblRoomInfo();
                        room.RoomID = Convert.ToInt32(rdr["RoomID"]);
                        room.Floor = rdr["Floor"].ToString();
                        room.RoomNo = rdr["RoomNo"].ToString();
                        room.NoOfBed = Convert.ToInt32(rdr["NoOfBed"]);
                        room.NoOfAdult = Convert.ToInt32(rdr["NoOfAdult"]);
                        room.NoOfChild = Convert.ToInt32(rdr["NoOfChild"]);
                        room.RoomType = rdr["RoomType"].ToString();
                        room.Status = rdr["Status"].ToString();
                        room.Description = rdr["RoomDescription"].ToString();
                        room.FeePerDay = Convert.ToDecimal(rdr["FeePerDay"]);
                        room.FeePerWeek = Convert.ToDecimal(rdr["FeePerWeek"]);
                        room.FeePerMonth = Convert.ToDecimal(rdr["FeePerMonth"]);
                        room.Facilities = rdr["Facilities"].ToString();
                        roomslist.Add(room);
                    }
                    con.Close();
                }
                return roomslist;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //To Add new employee record   
        public int AddRoom(tblRoomInfo room)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPRoomManager", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("OpCode", SqlDbType.NVarChar, 10).Value = "insert";
                    con.Open();
                    cmd.Parameters.AddWithValue("@RoomNo", room.RoomNo);
                    cmd.Parameters.AddWithValue("@Floor", room.Floor);
                    cmd.Parameters.AddWithValue("@NoOfBed", room.NoOfBed);
                    cmd.Parameters.AddWithValue("@NoOfAdult", room.NoOfAdult);
                    cmd.Parameters.AddWithValue("@NoOfChild", room.NoOfChild);
                    cmd.Parameters.AddWithValue("@RoomType", room.RoomType);
                    cmd.Parameters.AddWithValue("@RoomDescription", room.Description);
                    if (room.Facilities != "" && room.Facilities!=null)
                        cmd.Parameters.Add("FacilityName", SqlDbType.NVarChar).Value = room.Facilities.TrimEnd(',');
                    cmd.Parameters.Add("FeePerDay", SqlDbType.Decimal).Value = Convert.ToDecimal(room.FeePerDay);
                    cmd.Parameters.Add("FeePerWeek", SqlDbType.Decimal).Value = Convert.ToDecimal(room.FeePerWeek);
                    cmd.Parameters.Add("FeePerMonth", SqlDbType.Decimal).Value = Convert.ToDecimal(room.FeePerMonth);
                    cmd.Parameters.Add("Status", SqlDbType.NVarChar).Value = room.Status.ToString();
                    cmd.Parameters.Add("PercentageChange", SqlDbType.Decimal).Value = Convert.ToDecimal(room.PercentageChange);
                    if (room.Image!=null)
                        cmd.Parameters.Add("Image", SqlDbType.NVarChar).Value = room.Image.ToString(); 
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //To Update the records of a particluar employee  
        public int UpdateRoom(tblRoomInfo room)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPRoomManager", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("OpCode", SqlDbType.NVarChar, 10).Value = "update";
                    cmd.Parameters.Add("RoomID", SqlDbType.Int).Value = room.RoomID;
                    cmd.Parameters.Add("Floor", SqlDbType.NVarChar, 50).Value = room.Floor;
                    cmd.Parameters.Add("RoomNo", SqlDbType.NVarChar, 50).Value =room.RoomNo;
                    cmd.Parameters.Add("NoOfBed", SqlDbType.Int).Value = room.NoOfBed;
                    cmd.Parameters.Add("NoOfAdult", SqlDbType.Int).Value = room.NoOfAdult;
                    cmd.Parameters.Add("NoOfChild", SqlDbType.Int).Value = room.NoOfChild;
                    cmd.Parameters.Add("RoomDescription", SqlDbType.NVarChar).Value = room.Description;
                    if (room.Facilities != "" && room.Facilities != null)
                        cmd.Parameters.Add("FacilityName", SqlDbType.NVarChar).Value = room.Facilities.TrimEnd(',');
                    cmd.Parameters.Add("FeePerDay", SqlDbType.Decimal).Value = Convert.ToDecimal(room.FeePerDay);
                    cmd.Parameters.Add("FeePerWeek", SqlDbType.Decimal).Value = Convert.ToDecimal(room.FeePerWeek);
                    cmd.Parameters.Add("FeePerMonth", SqlDbType.Decimal).Value = Convert.ToDecimal(room.FeePerMonth);
                    cmd.Parameters.Add("Status", SqlDbType.NVarChar).Value = room.Status.ToString();
                    cmd.Parameters.Add("PercentageChange", SqlDbType.Decimal).Value = Convert.ToDecimal(room.PercentageChange);
                    if (room.Image != null)
                        cmd.Parameters.Add("Image", SqlDbType.NVarChar).Value = room.Image.ToString();
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //Get the details of a particular employee  
        public tblRoomInfo GetRoom(int id)
        {
            try
            {
                tblRoomInfo room = new tblRoomInfo();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPRoomManager", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("OpCode", SqlDbType.NVarChar, 10).Value = "selectbyid";
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        room.RoomID = Convert.ToInt32(rdr["RoomID"]);
                        room.Floor = rdr["Floor"].ToString();
                        room.RoomNo = rdr["RoomNo"].ToString();
                        room.NoOfBed = Convert.ToInt32(rdr["NoOfBed"]);
                        room.NoOfAdult = Convert.ToInt32(rdr["NoOfAdult"]);
                        room.NoOfChild = Convert.ToInt32(rdr["NoOfChild"]);
                        room.RoomType = rdr["RoomType"].ToString();
                        room.Status = (rdr["Status"]).ToString();
                        room.Description = rdr["RoomDescription"].ToString();
                        room.FeePerDay = Convert.ToDecimal(rdr["FeePerDay"]);
                        room.FeePerWeek = Convert.ToDecimal(rdr["FeePerWeek"]);
                        room.FeePerMonth = Convert.ToDecimal(rdr["FeePerMonth"]);
                    }
                }
                return room;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        //To Delete the record on a particular employee  
        public int DeleteRoom(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPRoomManager", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("OpCode", SqlDbType.NVarChar, 10).Value = "delete";
                    cmd.Parameters.Add("RoomID", SqlDbType.Int).Value = id;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public tblHotels GetHotelDetails(string licencenumber)
        {
            try
            {
                tblHotels hotel = new tblHotels();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlQuery = "SELECT * FROM tblHotels WHERE LisenceNo = '" + licencenumber + "'";
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        hotel.ID = Convert.ToInt32(rdr["ID"]);
                        hotel.HotelName = rdr["HotelName"].ToString();
                        hotel.HotelAddress = rdr["HotelAddress"].ToString();
                        hotel.LisenceNo = rdr["LisenceNo"].ToString();
                        hotel.Logo = rdr["Logo"].ToString();
                        hotel.PhoneNumberFirst = rdr["PhoneNumberFirst"].ToString();
                        hotel.PhoneNumberSecond = rdr["PhoneNumberSecond"].ToString();
                        hotel.Country = rdr["Country"].ToString();
                        hotel.State = rdr["State"].ToString();
                        hotel.City = rdr["City"].ToString();
                        hotel.ZipCode = rdr["ZipCode"].ToString();
                        hotel.HeaderNotes = rdr["HeaderNotes"].ToString();
                        hotel.FooterNotes = rdr["FooterNotes"].ToString();
                        hotel.SpecialNotes = rdr["SpecialNotes"].ToString();
                    }
                }
                return hotel;
            }
            catch
            {
                throw;
            }
        }

        public int UpdateHotel(tblHotels hotel)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("tblHotelsUpdate", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("ID", SqlDbType.Int).Value = hotel.ID;
                    cmd.Parameters.Add("HotelName", SqlDbType.NVarChar, 50).Value = hotel.HotelName;
                    cmd.Parameters.Add("@HotelAddress", SqlDbType.NVarChar, 50).Value = hotel.HotelAddress;
                    cmd.Parameters.Add("@LisenceNo", SqlDbType.NVarChar, 50).Value = hotel.LisenceNo;
                    cmd.Parameters.Add("@Logo", SqlDbType.NVarChar, 500).Value = hotel.Logo;
                    cmd.Parameters.Add("@PhoneNumberFirst", SqlDbType.NVarChar, 50).Value = hotel.PhoneNumberFirst;
                    cmd.Parameters.Add("@PhoneNumberSecond", SqlDbType.NVarChar, 50).Value = hotel.LisenceNo;
                    cmd.Parameters.Add("@Country", SqlDbType.NVarChar, 50).Value = hotel.Country;
                    cmd.Parameters.Add("@State", SqlDbType.NVarChar, 50).Value = hotel.State;
                    cmd.Parameters.Add("@City", SqlDbType.NVarChar, 50).Value = hotel.City;
                    cmd.Parameters.Add("@ZipCode", SqlDbType.NVarChar, 50).Value = hotel.ZipCode;
                    cmd.Parameters.Add("@HeaderNotes", SqlDbType.NVarChar, 50).Value = hotel.HeaderNotes;
                    cmd.Parameters.Add("@FooterNotes", SqlDbType.NVarChar, 50).Value = hotel.FooterNotes;
                    cmd.Parameters.Add("@SpecialNotes", SqlDbType.NVarChar, 50).Value = hotel.SpecialNotes;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public IEnumerable<tblRoomInfo> ShowBookedRooms()
        {
            try
            {
                List<tblRoomInfo> roomslist = new List<tblRoomInfo>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlQuery = @"
                                    SELECT *
                                      FROM [HMSDB].[dbo].[viewBookedRooms]";
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        tblRoomInfo room = new tblRoomInfo();
                        room.RoomID = Convert.ToInt32(rdr["RoomID"]);
                        room.RoomNo = rdr["RoomNo"].ToString();
                        room.Floor = rdr["Floor"].ToString();
                        room.CustomerName = rdr["CustomerName"].ToString();
                        room.FromeDateTime = Convert.ToDateTime(rdr["FromeDateTime"]);
                        room.ToDateTime = Convert.ToDateTime(rdr["ToDateTime"]);
                        room.RoomType = rdr["RoomType"].ToString();
                        room.RemainingDays = Convert.ToInt32(rdr["RemainingDays"]);
                        roomslist.Add(room);
                    }
                    con.Close();
                }
                return roomslist;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int UpdateBookedRoom(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string sqlQuery = "UPDATE tblRoom SET Status = 'Available' WHERE ID =" + id;
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                }
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        public IEnumerable<tblRoomInfo> FilterRooms(tblRoomInfo filters)
        {
            try
            {
                List<tblRoomInfo> roomslist = new List<tblRoomInfo>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPRoomFilter", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (filters.Floor != "All" && filters.Floor != "")
                        cmd.Parameters.Add("Floor", SqlDbType.NVarChar, 50).Value = filters.Floor;
                    if (filters.RoomNo != "All" && filters.RoomNo != "")
                        cmd.Parameters.Add("RoomNo", SqlDbType.NVarChar, 50).Value = filters.RoomNo;
                    cmd.Parameters.Add("Available", SqlDbType.NVarChar, 50).Value = filters.Status;
                    cmd.Parameters.Add("@PriceFrom", SqlDbType.NVarChar,50).Value = DBNull.Value;
                    cmd.Parameters.Add("@PriceTo", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                    if (filters.RoomType != "All" && filters.RoomType!=null)
                        cmd.Parameters.Add("@RoomType", SqlDbType.NVarChar, 50).Value = filters.RoomType;
                    if (!filters.Facilities.Contains("All") || (!filters.Facilities.Contains(" ")))
                    {
                        filters.Facilities = filters.Facilities.Replace(" ","");
                        if(filters.Facilities.Length> 0 )
                        {
                        cmd.Parameters.Add("@Facilities", SqlDbType.NVarChar, 1000).Value = "'" + String.Join("','", filters.Facilities.TrimEnd(',').Split(",")) + "'"; 
                        }
                    }
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        tblRoomInfo room = new tblRoomInfo();
                        room.RoomID = Convert.ToInt32(rdr["RoomID"]);
                        room.Floor = rdr["Floor"].ToString();
                        room.RoomNo = rdr["RoomNo"].ToString();
                        room.NoOfBed = Convert.ToInt32(rdr["NoOfBed"]);
                        room.NoOfAdult = Convert.ToInt32(rdr["NoOfAdult"]);
                        room.NoOfChild = Convert.ToInt32(rdr["NoOfChild"]);
                        room.RoomType = rdr["RoomType"].ToString();
                        room.Status = rdr["Status"].ToString();
                        room.Description = rdr["Description"].ToString();
                        room.FeePerDay = Convert.ToDecimal(rdr["FeePerDay"]);
                        room.FeePerWeek = Convert.ToDecimal(rdr["FeePerWeek"]);
                        room.FeePerMonth = Convert.ToDecimal(rdr["FeePerMonth"]);
                        room.Facilities = rdr["Facilities"].ToString();
                        room.Image = rdr["Image"].ToString();
                        roomslist.Add(room);
                    }
                    con.Close();
                }
                return roomslist;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public IEnumerable<tblRoomBooking> FilterRoomsbyCustomer(tblRoomBooking filters)
        {
            try
            {
                List<tblRoomBooking> roomslist = new List<tblRoomBooking>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPRoomFilter", con);
                    //cmd.CommandType = CommandType.StoredProcedure;
                    //if (filters.Floor != "All" && filters.Floor != "")
                    //    cmd.Parameters.Add("Floor", SqlDbType.NVarChar, 50).Value = filters.Floor;
                    //if (filters.RoomNo != "All" && filters.RoomNo != "")
                    //    cmd.Parameters.Add("RoomNo", SqlDbType.NVarChar, 50).Value = filters.RoomNo;
                    //cmd.Parameters.Add("Available", SqlDbType.NVarChar, 50).Value = filters.Status;
                    //cmd.Parameters.Add("@PriceFrom", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                    //cmd.Parameters.Add("@PriceTo", SqlDbType.NVarChar, 50).Value = DBNull.Value;
                    //if (filters.RoomType != "All" && filters.RoomType != null)
                    //    cmd.Parameters.Add("@RoomType", SqlDbType.NVarChar, 50).Value = filters.RoomType;
                    //if (!filters.Facilities.Contains("All") || (!filters.Facilities.Contains(" ")))
                    //{
                    //    filters.Facilities = filters.Facilities.Replace(" ", "");
                    //    if (filters.Facilities.Length > 0)
                    //    {
                    //        cmd.Parameters.Add("@Facilities", SqlDbType.NVarChar, 1000).Value = "'" + String.Join("','", filters.Facilities.TrimEnd(',').Split(",")) + "'";
                    //    }
                    //}
                    //con.Open();
                    //SqlDataReader rdr = cmd.ExecuteReader();

                    //while (rdr.Read())
                    //{
                    //    tblRoomInfo room = new tblRoomInfo();
                    //    room.RoomID = Convert.ToInt32(rdr["RoomID"]);
                    //    room.Floor = rdr["Floor"].ToString();
                    //    room.RoomNo = rdr["RoomNo"].ToString();
                    //    room.NoOfBed = Convert.ToInt32(rdr["NoOfBed"]);
                    //    room.NoOfAdult = Convert.ToInt32(rdr["NoOfAdult"]);
                    //    room.NoOfChild = Convert.ToInt32(rdr["NoOfChild"]);
                    //    room.RoomType = rdr["RoomType"].ToString();
                    //    room.Status = rdr["Status"].ToString();
                    //    room.Description = rdr["Description"].ToString();
                    //    room.FeePerDay = Convert.ToDecimal(rdr["FeePerDay"]);
                    //    room.FeePerWeek = Convert.ToDecimal(rdr["FeePerWeek"]);
                    //    room.FeePerMonth = Convert.ToDecimal(rdr["FeePerMonth"]);
                    //    room.Facilities = rdr["Facilities"].ToString();
                    //    room.Image = rdr["Image"].ToString();
                    //    roomslist.Add(room);
                    //}
                    //con.Close();
                }
                return roomslist;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #region Facility
        public IEnumerable<tblFacility> GetFacilities()
        {
            try
            {
                List<tblFacility> facilitylist = new List<tblFacility>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPFacilityManager", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("OpCode", SqlDbType.NVarChar, 10).Value = "selectall";
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        tblFacility fac = new tblFacility();
                        fac.ID = Convert.ToInt32(rdr["ID"]);
                        fac.IsAvailable = Convert.ToBoolean(rdr["IsAvailable"]);
                        fac.FacilityName = rdr["FacilityName"].ToString();
                        facilitylist.Add(fac);
                    }
                    con.Close();
                }
                return facilitylist;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public int DeleteFacility(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPFacilityManager", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("OpCode", SqlDbType.NVarChar, 10).Value = "delete";
                    cmd.Parameters.Add("FacilityId", SqlDbType.Int).Value = id;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int UpdateFacility(tblFacility fac)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPFacilityManager", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("OpCode", SqlDbType.NVarChar, 10).Value = "update";
                    cmd.Parameters.Add("FacilityId", SqlDbType.Int).Value = fac.ID;
                    cmd.Parameters.Add("FacilityName", SqlDbType.NVarChar, 200).Value = fac.FacilityName;
                    cmd.Parameters.Add("IsAvailable", SqlDbType.Bit).Value = fac.IsAvailable;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int AddFacility(tblFacility fac)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SPFacilityManager", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("OpCode", SqlDbType.NVarChar, 10).Value = "insert";
                    con.Open();
                    cmd.Parameters.AddWithValue("@FacilityId", fac.ID);
                    cmd.Parameters.AddWithValue("@FacilityName", fac.FacilityName);
                    cmd.Parameters.AddWithValue("@IsAvailable", fac.IsAvailable);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        #endregion

        //Rooking Booking Region: Ctrl + K + S
        #region Room Booking
        public int RoomBookingByAdmin(tblRoomInfo bookingdetails)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("SpRoomBooker", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    cmd.Parameters.AddWithValue("@RoomId", bookingdetails.RoomID);
                    cmd.Parameters.AddWithValue("@FirstName", bookingdetails.FirstName);
                    cmd.Parameters.AddWithValue("@MiddleName", bookingdetails.MiddleName);
                    cmd.Parameters.AddWithValue("@LastName", bookingdetails.LastName);
                    cmd.Parameters.AddWithValue("@PhoneNo", bookingdetails.ContactNumber);
                    cmd.Parameters.AddWithValue("@IDNumber", bookingdetails.IdCardNumber);
                    cmd.Parameters.AddWithValue("@Address", bookingdetails.Address);
                    cmd.Parameters.AddWithValue("@CountryName", bookingdetails.Country);
                    cmd.Parameters.AddWithValue("@CityName", bookingdetails.City);

                    cmd.Parameters.AddWithValue("@FromeDateTime", bookingdetails.FromeDateTime);
                    cmd.Parameters.AddWithValue("@ToDateTime", bookingdetails.ToDateTime);
                    cmd.Parameters.AddWithValue("@BookingDate", bookingdetails.BookingDate);
                    cmd.Parameters.AddWithValue("@PaymentType", bookingdetails.PaymentStatus);
                    cmd.Parameters.Add("AdvancedPayment", SqlDbType.Decimal).Value = Convert.ToDecimal(bookingdetails.AdvancePayment);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        #endregion
    }
}