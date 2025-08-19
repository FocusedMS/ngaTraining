import React, { useEffect, useState } from "react";
import api from "../api/axios";
import ContactForm from "../components/ContactForm";

export default function Contacts() {
  const [contacts, setContacts] = useState([]);
  const [message, setMessage] = useState("");
  const [editingContact, setEditingContact] = useState(null);
  const [loading, setLoading] = useState(false);

  async function loadContacts() {
    setLoading(true);
    try {
      const res = await api.get("/contacts");
      setContacts(res.data);
      setLoading(false);
    } catch (err) {
      setLoading(false);
      setMessage(err.response?.data?.message || "Failed to load contacts");
    }
  }

  useEffect(() => {
    loadContacts();
  }, []);

  async function handleCreate(contact) {
    try {
      const res = await api.post("/contacts", contact);
      setContacts((prev) => [...prev, res.data]);
      setMessage("Contact added");
    } catch (err) {
      setMessage(err.response?.data?.message || "Failed to add contact");
    }
  }

  async function handleUpdate(contact) {
    try {
      const res = await api.put(`/contacts/${contact.id}`, contact);
      setContacts((prev) => prev.map((c) => (c.id === contact.id ? res.data : c)));
      setEditingContact(null);
      setMessage("Contact updated");
    } catch (err) {
      setMessage(err.response?.data?.message || "Failed to update contact");
    }
  }

  async function handleDelete(id) {
    try {
      await api.delete(`/contacts/${id}`);
      setContacts((prev) => prev.filter((c) => c.id !== id));
      setMessage("Contact deleted");
    } catch (err) {
      setMessage(err.response?.data?.message || "Failed to delete contact");
    }
  }

  return (
    <div>
      <div className="card">
        <h2>{editingContact ? "Edit Contact" : "Add Contact"}</h2>
        <ContactForm
          key={editingContact ? editingContact.id : "new"}
          initialValues={editingContact || { name: "", email: "", phone: "" }}
          onSubmit={editingContact ? handleUpdate : handleCreate}
          onCancel={() => setEditingContact(null)}
          isEditing={!!editingContact}
        />
      </div>

      <div className="card">
        <h2>Your Contacts</h2>
        {message && <div className="info">{message}</div>}
        {loading ? (
          <div>Loading...</div>
        ) : (
          <ul className="list">
            {contacts.map((c) => (
              <li key={c.id} className="list-item">
                <div>
                  <div className="contact-name">{c.name}</div>
                  <div className="contact-details">{c.email} • {c.phone}</div>
                </div>
                <div className="actions">
                  <button onClick={() => setEditingContact(c)}>Edit</button>
                  <button className="danger" onClick={() => handleDelete(c.id)}>Delete</button>
                </div>
              </li>
            ))}
            {contacts.length === 0 && <li>No contacts yet.</li>}
          </ul>
        )}
      </div>
    </div>
  );
} 