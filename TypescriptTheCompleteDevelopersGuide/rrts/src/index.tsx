import React from "react";
import ReactDOM from "react-dom";

interface IProps {
  color?: string;
}

interface IState {
  counter: number;
}

const AppFunctionComponent = (props: IProps): JSX.Element => {
  return <div>{props.color}</div>;
};

class App extends React.Component<IProps, IState> {
  constructor(props: IProps) {
    super(props);

    this.state = { counter: 0 };
  }

  onIncrement = (): void => {
    this.setState({ counter: this.state.counter + 1 });
  };

  onDecrement = (): void => {
    this.setState({ counter: this.state.counter - 1 });
  };

  render() {
    return (
      <div>
        <button onClick={this.onIncrement}>Increment</button>
        <button onClick={this.onDecrement}>Decrement</button>
        {this.state.counter}
      </div>
    );
  }
}

// ReactDOM.render(<AppFunctionComponent />, document.querySelector("#root"));
ReactDOM.render(<App />, document.querySelector("#root"));
