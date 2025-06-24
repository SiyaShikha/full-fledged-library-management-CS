import React, { useState } from "react";
import BookList from "./BookList";

const FindBook = ({ dispatch }) => {
  const [bookId, setBookId] = useState("");
  const [book, setBook] = useState(null);
  const [error, setError] = useState("");

  const handleSearch = async () => {
    if (!bookId) {
      setError("Please enter a book ID.");
      return;
    }

    try {
      const response = await fetch(`http://localhost:5008/api/books/${bookId}`);
      setBook(await response.json());
      dispatch({ type: "FIND_BOOK", payload: bookId });
    } catch (err) {
      setError("Couldn't find a book.");
      setBook(null);
    }
  };

  return (
    <div>
      <h2>Find a Book</h2>
      <input
        type="number"
        placeholder="Enter Book ID"
        value={bookId}
        onChange={(e) => setBookId(e.target.value)}
      />
      <button onClick={handleSearch}>Search</button>

      {error && <p>{error}</p>}

      {book && (
        <div>
          <h3>Book Details</h3>
          <p>Title: {book.title}</p>
          <p>Author: {book.author}</p>
          <p>Genre: {book.genre}</p>
          <p>Year: {book.year}</p>
        </div>
      )}
    </div>
  );
};

export default FindBook;
