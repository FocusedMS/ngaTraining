"use strict";
const express = require("express");
const { contactsByUserId } = require("../data/store");
const { v4: uuidv4 } = require("uuid");

const router = express.Router();

router.get("/", (req, res) => {
  const userId = req.user.userId;
  const contacts = contactsByUserId.get(userId) || [];
  return res.json(contacts);
});

router.post("/", (req, res) => {
  const userId = req.user.userId;
  const { name, email, phone } = req.body || {};
  if (!name || !email || !phone) {
    return res
      .status(400)
      .json({ message: "Name, email, and phone are required" });
  }
  const contact = { id: uuidv4(), name, email, phone };
  const contacts = contactsByUserId.get(userId) || [];
  contacts.push(contact);
  contactsByUserId.set(userId, contacts);
  return res.status(201).json(contact);
});

router.put("/:id", (req, res) => {
  const userId = req.user.userId;
  const { id } = req.params;
  const { name, email, phone } = req.body || {};
  const contacts = contactsByUserId.get(userId) || [];
  const idx = contacts.findIndex((c) => c.id === id);
  if (idx === -1) {
    return res.status(404).json({ message: "Contact not found" });
  }
  const updated = {
    ...contacts[idx],
    name: name ?? contacts[idx].name,
    email: email ?? contacts[idx].email,
    phone: phone ?? contacts[idx].phone,
  };
  contacts[idx] = updated;
  contactsByUserId.set(userId, contacts);
  return res.json(updated);
});

router.delete("/:id", (req, res) => {
  const userId = req.user.userId;
  const { id } = req.params;
  const contacts = contactsByUserId.get(userId) || [];
  const exists = contacts.some((c) => c.id === id);
  if (!exists) {
    return res.status(404).json({ message: "Contact not found" });
  }
  const filtered = contacts.filter((c) => c.id !== id);
  contactsByUserId.set(userId, filtered);
  return res.status(204).send();
});

module.exports = router; 