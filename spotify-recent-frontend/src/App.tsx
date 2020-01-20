import React from 'react';
import './App.css';
import { History } from './History'
import Song from './Song';
import SongList from './SongList';

interface IProps {
}
interface IState {
  history?: History,
}

class App extends React.Component<IProps, IState> {
  constructor(props: IProps) {
    super(props);
    this.state = {
        history: undefined,
    }
  }

componentDidMount() {
    fetch("https://galenguyer.com/api/spotify")
    .then(result => {
        return result.text();    
    })
    .then(result => {
        let newHistory: History = JSON.parse(result);
        this.setState({history: newHistory})
    });
  }

  render() {
    return (
      <div className="App">
        <header className="App-header">
          <h1>Recent Songs</h1>
        </header>
        {
            this.state.history != undefined ? 
              this.state.history.currentTrack != undefined ?
                <Song
                    id={"currentSong" + (this.state.history.currentTrack.isPlaying ? "isPlaying" : "isPaused")}
                    name={this.state.history.currentTrack.name} 
                    artists={this.state.history.currentTrack.artists}
                    link={this.state.history.currentTrack.ytSongLink} />
            : undefined : undefined
        }
        <SongList tracks={this.state.history !== undefined ? this.state.history.tracks : undefined}/>
        <p className="note">Read a bit about this project <a href="https://galenguyer.com/projects#spotify">here!</a></p>
      </div>
    );
  }
}

export default App;
