import React, { ReactElement, ReactNode } from "react";
import "../styles.css";

function App({ children }: {children: ReactNode}) {
  return (
    <>{children}</>
  );
}

export default App;
