import React, { useState } from "react";

export default function ContactForm({ initialValues, onSubmit, onCancel, isEditing }) {
  const [name, setName] = useState(initialValues.name || "");
  const [email, setEmail] = useState(initialValues.email || "");
  const [phone, setPhone] = useState(initialValues.phone || "");
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  async function handleSubmit(e) {
    e.preventDefault();
    setError("");
    setLoading(true);
    try {
      await onSubmit({ ...initialValues, name, email, phone });
      setLoading(false);
      if (!isEditing) {
        setName("");
        setEmail("");
        setPhone("");
      }
    } catch (err) {
      setLoading(false);
      setError(err?.message || "Failed to submit");
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      {error && <div className="alert">{error}</div>}
      <label>
        Name
        <input value={name} onChange={(e) => setName(e.target.value)} required />
      </label>
      <label>
        Email
        <input type="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
      </label>
      <label>
        Phone
        <input value={phone} onChange={(e) => setPhone(e.target.value)} required />
      </label>
      <div className="form-actions">
        {isEditing && (
          <button type="button" onClick={onCancel}>
            Cancel
          </button>
        )}
        <button type="submit" disabled={loading}>
          {loading ? "Saving..." : isEditing ? "Update" : "Add"}
        </button>
      </div>
    </form>
  );
} 