import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import Navbar from './navbar.tsx'


createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
)

createRoot(document.getElementById('navbar')!).render(
    <StrictMode>
        <Navbar />
    </StrictMode>,
)
