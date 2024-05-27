import "reflect-metadata";
import React from "react";
import ReactDOM from "react-dom/client";
import { Router } from "./View/routes";
import "./styles.css";
import { ApolloClient, ApolloProvider, InMemoryCache } from "@apollo/client";
const client = new ApolloClient({
    uri: (new URL("/graphql", "https://localhost:5081")).toString(),
    cache: new InMemoryCache(),
    headers: {
        "Authorization": "Bearer " + localStorage.getItem("token"),
    }
  });

ReactDOM.createRoot(document.getElementById("root") as HTMLElement).render(

    <React.StrictMode>
        <ApolloProvider client={client}>
                <Router />
        </ApolloProvider>
    </React.StrictMode>,
);
