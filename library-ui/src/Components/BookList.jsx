import React from "react";

const BookList = ({ dispatch, books, loading, error }) => {

    const handleDelete = async (id) => {
        try {
            await fetch(`http://localhost:5008/api/books/${id}`, {
                method: "DELETE",
            });
            dispatch({ type: "DELETE_BOOK", payload: id });
        } catch (error) {
            console.error("Error deleting book:", error);
        }
    };

   if (loading) return <p>Loading...</p>;
   if (error) return <p>Error: {error}</p>;

   return (
       <div>
           <h2>Book List</h2>
           <ul>
               {books.map((book) => (
                   <li key={book.id}>
                       {book.title} by {book.author} ({book.year})
                       <button onClick={() => handleDelete(book.id)}>Delete</button>
                   </li>
               ))}
           </ul>
       </div>
   );
};

export default BookList;
