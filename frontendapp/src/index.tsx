import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import Home from './Home.tsx'
import Navbar from './navbar/navbar.tsx'


createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <Home />
  </StrictMode>,
)

createRoot(document.getElementById('navbar')!).render(
    <StrictMode>
        <Navbar />
    </StrictMode>,
)
