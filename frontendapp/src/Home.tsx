import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './Home.css'

function Home() {
    return (
        <div className="container">
            <div className="content-container">
                <div className = "img-container">
                    <img src="./src/assets/MountinDark.jpg" className="img-home" alt="Home image" />
                </div>
                <div className="text-content-container">
                    <h1 className="home-title">HealthCareAB</h1>
                    <p className="home-text">Example text example text example text. example text example text example text example text</p>
                </div>
            </div>
        </div>
  )
}

export default Home
