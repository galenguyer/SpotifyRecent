import React from 'react';
import './App.css';
import { History } from './History'
import Song from './Song';

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
            this.state.history != null ? 
              this.state.history.currentTrack != null ?
                <Song
                    id="currentSong"
                    name={this.state.history.currentTrack.name} 
                    artists={this.state.history.currentTrack.artists} />
            : undefined : undefined
        }
        <div className="songlist">
        {this.state.history != null ? this.state.history.tracks.map((track)=> {
          return (<Song name={track.name} artists={track.artists}/>)
        }): undefined}
        </div>
      </div>
    );
  }
}

export default App;
