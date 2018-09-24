# ZupaTech
Task for ZupaTech
zupaTech Developer test

# Development of a seat booking system for a set of meetings

Database

There will be 2 database tables to hold the meetings and related seats.  A table of users could be added to allow this design to be extended
Meeting table :
	Id – unique id
	Date – date of the meeting
Seat table :
	Id – unique id
	Row – seat row
	Col – number of the seat in the row
	Email – email address of the booked seat
	MeetingId – foreign key to link to the meeting record
  
# Web service to provide access to the database

A web service is provided to read and update the database.  This will provide the following functions :
URL		
/meetings	GET	Get a list of all the meetings

/meetings/{id}	GET	Get a specific meeting by Id

/meetings/{date}	GET	Get a meeting by date

/meetings	POST	Create a new meeting in the database
The meeting date must no already exist

/meetings/{id}	PUT	Update a meeting
The meeting date can be changed as long as it is still unique

/meetings/{id}	DELETE	Remove a meeting from the database
This should also remove and seats that have already been booked

/seats/	GET	Get a list of all booked seats

/seats/{id}	GET	Get a single booked seat by Id

/seats/{meetingdate}	GET	Get all booked seats for a meeting

/seats/{email}	GET	Get seats booked for a specific email address

/seats/{row}/{seat}/{meetingdate}	GET	Get a single seat booking for a meeting

/seats	POST	Book a single seat
Seat must not be booked already
The email address must be unique in this meeting
Seat position must exist

/seats/{id}	PUT	Update the booking for a seat
Should only be able to change the email address

/seats/{id}	DELETE	Remove a seat booking


# Future Enhancements

A table of Users would provide some contact details of each person.

Cost per seat could be added to Meetings table.

Details of amount and date of payment can be added to the Seats table

