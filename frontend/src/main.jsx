import React from "react"
import { BrowserRouter, Routes, Route } from "react-router-dom"
import ReactDOM from "react-dom/client"
import App from "./App.jsx"
import "./index.css"
import { AuthProvider } from "./contexts/AuthContext.jsx"
import { ToastProvider } from "./contexts/ToastContext.jsx"

ReactDOM.createRoot(document.getElementById("root")).render(
  <React.StrictMode>
    <BrowserRouter>
      <AuthProvider>
        <ToastProvider>
          <Routes>
            <Route path='/*' element={<App />} />
          </Routes>
        </ToastProvider>
      </AuthProvider>
    </BrowserRouter>
  </React.StrictMode>
)
