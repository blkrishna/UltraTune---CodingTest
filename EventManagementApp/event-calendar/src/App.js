import React, { useState } from "react";
import FullCalendar from "@fullcalendar/react";
import dayGridPlugin from "@fullcalendar/daygrid";
import interactionPlugin from "@fullcalendar/interaction";
import timeGridPlugin from "@fullcalendar/timegrid";
import "bootstrap/dist/css/bootstrap.min.css";
import { Tooltip } from "react-tooltip"; // Correct named import

const App = () => {
    // State to manage events
    const [events, setEvents] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [formData, setFormData] = useState({
        title: "",
        description: "",
        eventDate: "",
    });

    // Handle input change for event creation form
    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    // Handle form submission to add an event
    const handleSubmit = (e) => {
        e.preventDefault();
        if (!formData.title || !formData.eventDate) {
            alert("Please provide title and date.");
            return;
        }

        const newEvent = {
            id: Date.now().toString(),
            title: formData.title,
            start: formData.eventDate,
            extendedProps: {
                description: formData.description,
            },
        };

        setEvents([...events, newEvent]);
        setFormData({ title: "", description: "", eventDate: "" });
    };

    // Handle event click to delete an event
    const handleEventClick = (clickInfo) => {
        if (window.confirm(`Delete event "${clickInfo.event.title}"?`)) {
            setEvents(events.filter((event) => event.id !== clickInfo.event.id));
        }
    };

    // Filter events based on search term
    const filteredEvents = events.filter(
        (event) =>
            event.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
            (event.extendedProps.description &&
                event.extendedProps.description.toLowerCase().includes(searchTerm.toLowerCase()))
    );

    return (
        <div className="container mt-4">
            <h2 className="text-center">Event Management App</h2>

            {/* Event Form */}
            <form onSubmit={handleSubmit} className="mb-4">
                <div className="mb-2">
                    <input
                        type="text"
                        name="title"
                        placeholder="Event Title"
                        className="form-control"
                        value={formData.title}
                        onChange={handleChange}
                    />
                </div>
                <div className="mb-2">
                    <input
                        type="text"
                        name="description"
                        placeholder="Event Description"
                        className="form-control"
                        value={formData.description}
                        onChange={handleChange}
                    />
                </div>
                <div className="mb-2">
                    <input
                        type="datetime-local"
                        name="eventDate"
                        className="form-control"
                        value={formData.eventDate}
                        onChange={handleChange}
                    />
                </div>
                <button type="submit" className="btn btn-primary">Add Event</button>
            </form>

            {/* Search Field */}
            <input
                type="text"
                placeholder="Search Events..."
                className="form-control mb-3"
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)}
            />

            {/* FullCalendar Component */}
            <FullCalendar
                plugins={[dayGridPlugin, interactionPlugin, timeGridPlugin]}
                initialView="dayGridMonth"
                events={filteredEvents}
                eventClick={handleEventClick}
                eventContent={(eventInfo) => (
                    <div>
                        <b
                            data-tooltip-id={`tooltip-${eventInfo.event.id}`}
                            data-tooltip-content={eventInfo.event.extendedProps.description}
                        >
                            {eventInfo.event.title}
                        </b>
                        <Tooltip id={`tooltip-${eventInfo.event.id}`} />
                    </div>
                )}
            />
        </div>
    );
};

export default App;
