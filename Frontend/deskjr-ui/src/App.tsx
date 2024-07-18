import "./App.css";
import Login from "./components/views/Login";
import NavigationBar from "./components/CommonComponents/NavigationBar";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";

// App.tsx
import React from "react";

const navigation = {
  brand: { name: "NavigationBar", to: "/" },
  links: [
    { name: "About Me", to: "/about" },
    { name: "Blog", to: "/blog" },
    { name: "Development", to: "/dev" },
    { name: "Graphic Design", to: "/design" },
    { name: "Contact", to: "/contact" },
    { name: "Login", to: "/login" },
  ],
};

const App: React.FC = () => {
  const { brand, links } = navigation;

  return (
    <Router>
      <div className="App">
        <NavigationBar brand={navigation.brand} links={navigation.links} />
        <Routes>
          <Route path="/login" element={<Login />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;
