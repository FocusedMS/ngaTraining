import React, { useState } from "react";

export default function App() {
  const [customers, setCustomers] = useState([]);
  const [form, setForm] = useState({
    custId: "",
    custName: "",
    custUserName: "",
    password: "",
    city: "",
    state: "",
    email: "",
    mobileNo: "",
  });

  // load all customers
  const loadCustomers = async () => {
    const res = await fetch("/api/Customers");
    const data = await res.json();
    setCustomers(data);
  };

  // add new customer
  const addCustomer = async () => {
    const res = await fetch("/api/Customers", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ ...form, custId: Number(form.custId) }),
    });
    if (res.ok) {
      alert("Customer added ✅");
      loadCustomers();
    } else {
      alert("Error: " + (await res.text()));
    }
  };

  return (
    <div style={{ padding: 20 }}>
      <h2>Basic CMS Frontend</h2>

      <div>
        <input
          placeholder="custId"
          value={form.custId}
          onChange={(e) => setForm({ ...form, custId: e.target.value })}
        />
        <input
          placeholder="custName"
          value={form.custName}
          onChange={(e) => setForm({ ...form, custName: e.target.value })}
        />
        <input
          placeholder="custUserName"
          value={form.custUserName}
          onChange={(e) => setForm({ ...form, custUserName: e.target.value })}
        />
        <input
          placeholder="password"
          type="password"
          value={form.password}
          onChange={(e) => setForm({ ...form, password: e.target.value })}
        />
        <input
          placeholder="city"
          value={form.city}
          onChange={(e) => setForm({ ...form, city: e.target.value })}
        />
        <input
          placeholder="state"
          value={form.state}
          onChange={(e) => setForm({ ...form, state: e.target.value })}
        />
        <input
          placeholder="email"
          value={form.email}
          onChange={(e) => setForm({ ...form, email: e.target.value })}
        />
        <input
          placeholder="mobileNo"
          value={form.mobileNo}
          onChange={(e) => setForm({ ...form, mobileNo: e.target.value })}
        />
      </div>

      <button onClick={addCustomer} style={{ marginTop: 10 }}>
        Add Customer
      </button>

      <hr />

      <button onClick={loadCustomers}>Load Customers</button>
      <pre>{JSON.stringify(customers, null, 2)}</pre>
    </div>
  );
}
