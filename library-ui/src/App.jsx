import React, { useReducer, useEffect } from "react";
import BookList from "./Components/BookList";
import AddBook from "./Components/AddBook";

const initialState = {
  books: [],
  loading: false,
  error: null,
};

const reducer = (state, action) => {
  switch (action.type) {
    case "FETCH_INIT":
      return { ...state, loading: true, error: null };
    case "FETCH_SUCCESS":
      return { ...state, loading: false, books: action.payload };
    case "FETCH_FAILURE":
      return { ...state, loading: false, error: action.payload };
    case "ADD_BOOK":
      return { ...state, books: [...state.books, action.payload] };
    case "DELETE_BOOK":
      return {
        ...state,
        books: state.books.filter((book) => book.id !== action.payload),
      };
    default:
      throw new Error(`Unsupported action type: ${action.type}`);
  }
};

const App = () => {
  const [state, dispatch] = useReducer(reducer, initialState);

  // Fetch books from the API
  useEffect(() => {
    const fetchBooks = async () => {
      dispatch({ type: "FETCH_INIT" });

      try {
        const response = await fetch("http://localhost:5008/api/books");
        const data = await response.json();

        dispatch({ type: "FETCH_SUCCESS", payload: data });
      } catch (error) {
        console.log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>", error);
        dispatch({ type: "FETCH_FAILURE", payload: error.message });
      }
    };

    fetchBooks();
  }, []);

  return (
    <div>
      <h1>Library Management System</h1>
      <AddBook dispatch={dispatch} />
      <BookList
        dispatch={dispatch}
        books={state.books}
        loading={state.loading}
        error={state.error}
      />
    </div>
  );
};

export default App;
