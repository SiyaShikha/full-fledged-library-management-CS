import React, { useState } from "react";

const AddBook = ({ dispatch }) => {
  const [title, setTitle] = useState("");
  const [author, setAuthor] = useState("");
  const [genre, setGenre] = useState("");
  const [year, setYear] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    const newBook = {
      title,
      author,
      genre,
      year: parseInt(year, 10),
    };

    try {
      const response = await fetch("http://localhost:5008/api/books", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(newBook),
      });

      if (response.ok) {
        const savedBook = await response.json();
        dispatch({ type: "ADD_BOOK", payload: savedBook });
        alert("Book added successfully!");
        setTitle("");
        setAuthor("");
        setGenre("");
        setYear("");
      }
    } catch (error) {
      console.error("Error adding book:", error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Add a New Book</h2>
      <input
        type="text"
        placeholder="Title"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        required
      />
      <input
        type="text"
        placeholder="Author"
        value={author}
        onChange={(e) => setAuthor(e.target.value)}
        required
      />
      <input
        type="text"
        placeholder="Genre"
        value={genre}
        onChange={(e) => setGenre(e.target.value)}
        required
      />
      <input
        type="number"
        placeholder="Year"
        value={year}
        onChange={(e) => setYear(e.target.value)}
        required
      />
      <button type="submit">Add Book</button>
    </form>
  );
};

export default AddBook;
