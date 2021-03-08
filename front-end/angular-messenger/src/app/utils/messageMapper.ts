import { Chat } from '../models/data/chat.model';
import { Message } from '../models/data/message.model';
import { DateTime } from './dateTime';

export class MessageMapper {
  static mapMessaegesToChat(messages: Message[]): Chat[] {
    let chatHistory: Chat[] = [];
    let result: Map<string, Message[]> = new Map<string, Message[]>();

    messages.forEach((message) => {
      let date = DateTime.getDate(message.dateTime);
      result.set(date, []);
    });

    messages.forEach((message) => {
      let date = DateTime.getDate(message.dateTime);
      let time = DateTime.getTime(message.dateTime);

      let dateMessages = result.get(date);
      if (message.replyToId) {
        let reply = messages.find((msg) => msg.id == message.replyToId);
        if (reply) {
          message.replyToName = reply.composerName;
        }
      }
      dateMessages.push({
        ...message,
        dateTime: time
      });
      result.set(date, dateMessages);
    });

    result.forEach((msgArray, key) => {
      chatHistory.push({
        date: key,
        messages: msgArray.reverse()
      });
    });
    chatHistory = chatHistory.reverse();

    return chatHistory;
  }
}
